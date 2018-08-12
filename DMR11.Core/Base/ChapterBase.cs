using CloudFlareUtilities;
using DMR11.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.RegularExpressions;
using NLog;

namespace DMR11.Core
{
    [Serializable]
    public abstract class ChapterBase : IChapter
    {
        [NonSerialized]
        private CancellationToken _cancellationToken;

        [NonSerialized]
        private Task _task;

        [NonSerialized]
        private Progress<ChapterProgress> _progress;

        private ILogger _log;
        protected ILogger Log
        {
            get
            {
                return _log ?? (_log = LogManager.GetCurrentClassLogger());
            }
        }

        private string _saveDestination = string.Empty;
        private string _formattedChapterName = string.Empty;

        abstract protected List<Uri> ParsePageAddresses(string html);

        abstract protected List<Uri> ParseImageAddresses(string html);

        /// <summary>
        /// Gets or sets whether all the chapter's pages are in one webpage.
        /// The page listing is short-circuited if all of the 'pages'
        /// (chapter images) are in a single HTML page.
        /// </summary>
        public bool SinglePage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the chapter's name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the chapter's URI.
        /// </summary>
        public Uri Address
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets the location of where to save the downloaded chapter.
        /// </summary>
        public string SaveDestination
        {
            get
            {
                return _saveDestination;
            }
            set
            {
                _saveDestination = value;

                if (!string.IsNullOrWhiteSpace(_saveDestination))
                {
                    _saveDestination = Helper.FileSystem.GetSafePath(_saveDestination);
                }
            }
        }

        public string FormattedChapterName
        {
            get
            {
                //ChapterNameFormat = "{0} - Ch. {1}";

                if (_formattedChapterName == null)
                {
                    string name = Helper.FileSystem.GetSafeFileName(Name.Trim());
                    if (string.IsNullOrEmpty(ChapterNameFormat))
                    {
                        _formattedChapterName = name;
                    }
                    else
                    {
                        string formattedName = name;

                        //formattedName = string.Format(ChapterNameFormat, name, HostVariables["chapter"]);

                        _formattedChapterName = formattedName;
                    }
                }

                return _formattedChapterName;
            }
            set
            {
                _formattedChapterName = value;
            }
        }

        /// <summary>
        /// Gets or set the formatting used for creating the chapter directory.
        /// </summary>
        public string ChapterNameFormat
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a list of URIs representing the pages in the chapter.
        /// </summary>
        private List<Uri> ImageAddresses
        {
            get;
            set;
        }

        public string ChapterPath
        {
            get;
            set;
        }

        private bool NoChapterDirectory
        {
            get;
            set;
        }

        private Dictionary<string, string> _hostVariables;

        public Dictionary<string, string> HostVariables
        {
            get { return _hostVariables; }
        }

        public bool IsBusy
        {
            get
            {
                bool result = false;
                if (_task != null)
                {
                    switch (_task.Status)
                    {
                        case TaskStatus.Created:
                        case TaskStatus.Running:
                        case TaskStatus.WaitingForActivation:
                        case TaskStatus.WaitingForChildrenToComplete:
                        case TaskStatus.WaitingToRun:
                            result = true;
                            break;
                    }
                }
                return result;
            }
        }

        public IWebProxy Proxy { get; set; }

        public string Referrer { get; set; }

        public ChapterBase(string name, Uri address, ILogger log)
        {
            this._log = log;

            Name = name;
            Address = address;

            _hostVariables = new Dictionary<string, string>();
        }

        public Task DownloadImageAsync(string fileName, CancellationToken cancellationToken, Progress<ChapterProgress> progress)
        {
            _cancellationToken = cancellationToken;
            _progress = progress;
            SaveDestination = fileName;

            _task = Task.Factory.StartNew(() =>
            {
                _progress.ReportProgress(new ChapterProgress(this, 0));

                string html = DownloadString(Address);

                if (ImageAddresses == null)
                {
                    Console.WriteLine(this.Name);
                    PopulateImageAddress(html);
                }

                // Todo: Refactor.        

                ChapterPath = Path.Combine(SaveDestination, FormattedChapterName);
                if (!Directory.Exists(ChapterPath))
                {
                    Log.Info("Creating '{0}' directory", ChapterPath);
                    Directory.CreateDirectory(ChapterPath);
                }

                int countImage = 0;
                bool useAutoNumbering = false;

                foreach (var imageAddress in ImageAddresses)
                {
                    _cancellationToken.ThrowIfCancellationRequested();

                    // Todo: Check if path has an extension.                    
                    string pageFileName = Path.GetFileName(imageAddress.LocalPath);


                    if (!useAutoNumbering)
                    {
                        if (Regex.IsMatch(imageAddress.Host, "googleusercontent", RegexOptions.IgnoreCase | RegexOptions.Compiled))
                            useAutoNumbering = true;
                    }
                    else
                    {
                        if (!Regex.IsMatch(pageFileName, "\\.(png|jpg|jpeg)$", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                        {
                            pageFileName = Regex.Replace(imageAddress.Query, "\\?title=(.*)", "$1");
                        }
                    }

                    if (useAutoNumbering)
                    {
                        // Assume the extension is jpg since it's the most common. 
                        // Most image viewers will check the header and display it properly regardless
                        // of its extension. This is for Windows Explorer to display the thumbnail.
                        string ext = ".jpg";

                        pageFileName = string.Concat(countImage.ToString().PadLeft(3, '0'), ext);
                    }

                    string filePath = Path.Combine(ChapterPath, pageFileName); // saveToFolder + "\\" + pageFileName;

                    DownloadFile(imageAddress, filePath);

                    countImage++;
                    int percent = (countImage * 100 / ImageAddresses.Count / 2) + 50;
                    _progress.ReportProgress(new ChapterProgress(this, percent));
                }
            }, cancellationToken, TaskCreationOptions.None, TaskScheduler.Default);

            return _task;
        }

        private void PopulateImageAddress(string html)
        {
            Log.Trace("Entering Populate");
            //LogManager.

            if (SinglePage)
            {
                Log.Debug("Single page");
                ImageAddresses = ParseImageAddresses(html);
            }

            else
            {
                List<Uri> pageAddresses = ParsePageAddresses(html);
                Log.Trace("Pages in chapter: {pageAddress.Count}", pageAddresses.Count);

                var sbHtml = new StringBuilder();

                int countPage = 0;

                foreach (var pageAddress in pageAddresses)
                {
                    _cancellationToken.ThrowIfCancellationRequested();
                    string content = string.Empty;

                    int retryCount = 0;
                    const int MAX_RETRIES = 10;
                    const int RETRY_TIMEOUT = 1000;

                    do
                    {
                        try
                        {
                            content = DownloadString(pageAddress);
                            break;
                        }
                        catch (Exception)
                        {
                            // Todo: Log failure and retry count.
                            Log.Debug("Download failed, retrying. Retry count: {retryCount}", retryCount);

                            if (retryCount == MAX_RETRIES)
                                throw;
                            else
                                Thread.Sleep(RETRY_TIMEOUT * retryCount);
                        }

                    } while (retryCount++ != MAX_RETRIES);

                    sbHtml.AppendLine(content);

                    countPage++;
                    int percent = countPage * 100 / (pageAddresses.Count * 2);
                    _progress.ReportProgress(new ChapterProgress(this, percent));
                }

                ImageAddresses = ParseImageAddresses(sbHtml.ToString());
            }

        }

        private void DownloadFile(Uri address, string fileName)
        {
            Log.Debug("Downloading \"{0}\" as \"{1}\"", address.ToString(), fileName);

            Downloader.Instance.DownloadFile(address, fileName, _cancellationToken);
        }

        private string DownloadString(Uri address)
        {
            Log.Debug("Downloading string {0}", address.ToString());

            return Downloader.Instance.DownloadString(address);
        }

        /// <summary>
        /// A method called before the parsing the image addresses occur.
        /// </summary>
        virtual public void PreParseImageAddresses(params object[] options)
        {
        }

    }
}
