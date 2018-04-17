#define MISSED_KISS

using CloudFlareUtilities;
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

        abstract protected List<UriValidated> ParsePageAddresses(string html);

        abstract protected List<UriValidated> ParseImageAddresses(string html);
        
        /// <summary>
        /// Gets or sets whether all the chapter's pages are in one webpage.
        /// </summary>
        public bool SinglePage
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public UriValidated Address
        {
            get;
            protected set;
        }

        private List<UriValidated> ImageAddresses
        {
            get;
            set;
        }

        public string SaveDestination
        {
            get;
            set;
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

        public ChapterBase(string name, UriValidated address)
        {
            Name = name;
            Address = address;

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

                string saveToFolder = SaveDestination + "\\" + this.Name.Trim().RemoveFileNameInvalidChar();
                Directory.CreateDirectory(saveToFolder);

                int countImage = 0;
                bool useAutoNumbering = false;

                foreach (UriValidated imageAddress in ImageAddresses)
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

                    string filePath = saveToFolder + "\\" + pageFileName;

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
            // Single page. May not only apply to KissManga.
            if (SinglePage)
            {
                ImageAddresses = ParseImageAddresses(html);
            }

            else
            {
                List<UriValidated> pageAddresses = ParsePageAddresses(html);
                Console.WriteLine("Pages in chapter: {0}", pageAddresses.Count);

                var sbHtml = new StringBuilder();

                int countPage = 0;

                foreach (UriValidated pageAddress in pageAddresses)
                {
                    _cancellationToken.ThrowIfCancellationRequested();
                    string content = DownloadString(pageAddress);
                    sbHtml.AppendLine(content);

                    countPage++;
                    int percent = countPage * 100 / (pageAddresses.Count * 2);
                    _progress.ReportProgress(new ChapterProgress(this, percent));
                }

                ImageAddresses = ParseImageAddresses(sbHtml.ToString());
            }

        }

        private void DownloadFile(UriValidated address, string fileName)
        {
            Helper.Downloader.Instance.DownloadFile(address, fileName, _cancellationToken);
        }

        private string DownloadString(UriValidated address)
        {
            return Helper.Downloader.Instance.DownloadString(address);
        }


        // Todo: Implement a reusable http client to bypass the KissManga wait.

        HttpClient ReusableClient = null;
        ClearanceHandler ReusableKissHandler = null;

        private string DownloadStringR(UriValidated address)
        {
            // Todo: Optimize. Don't wrap entire code block in Try/Catch

            var result = new StringBuilder();
            var content = string.Empty;

            if (ReusableClient == null || ReusableKissHandler == null)
            {
                Console.WriteLine("Initializing new client.");
                
                ReusableKissHandler = new ClearanceHandler();

                ReusableClient = new HttpClient(ReusableKissHandler)
                {
                    Timeout = TimeSpan.FromSeconds(15)
                };
            }

            try
            {
                content = ReusableClient.GetStringAsync(address.ToString()).Result;
            }
            catch (Exception ex)
            {
                string error = String.Format("{0} - Error while download: {2} - Reason: {3}", DateTime.Now.ToLongTimeString(), this.Name, address.AbsoluteUri, ex.Message);
                throw new Exception(error, ex);
            }

            result.Append(content);

            return result.ToString();
        }

        /// <summary>
        /// A method called before the parsing the image addresses occur.
        /// </summary>
        virtual public void PreParseImageAddresses(params object[] options)
        {
        }
        
    }
}
