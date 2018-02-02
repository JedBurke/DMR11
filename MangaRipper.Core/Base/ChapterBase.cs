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

        private int UserAgentIndex = -1;

        string[] UserAgents = new string[] 
        {
            "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:40.0) Gecko/20100101 Firefox/40.1",
            "Mozilla/5.0 (Windows NT 6.2; Win64; x64; rv:27.0) Gecko/20121011 Firefox/27.0",
            "Mozilla/5.0 (X11; Linux x86_64; rv:28.0) Gecko/20100101 Firefox/28.0",
            "Mozilla/5.0 (Windows NT 6.1; rv:6.0) Gecko/20100101 Firefox/19.0",
            "Mozilla/5.0 (X11; Ubuntu; Linux armv7l; rv:17.0) Gecko/20100101 Firefox/17.0",
            "Mozilla/5.0 (Windows NT 6.1; rv:15.0) Gecko/20120716 Firefox/15.0a2",
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.8; rv:21.0) Gecko/20100101 Firefox/21.0",
            "Mozilla/5.0 (Windows NT 6.1; de;rv:12.0) Gecko/20120403211507 Firefox/12.0",
            "Mozilla/5.0 (Windows NT 6.1; rv:12.0) Gecko/20120403211507 Firefox/14.0.1",
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.6; rv:9.0) Gecko/20100101 Firefox/9.0",
            "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36",
            "Mozilla/5.0 (X11; OpenBSD i386) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.125 Safari/537.36",
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_3) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.1916.47 Safari/537.36",
            "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.517 Safari/537.36",
            "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.812.0 Safari/535.1",
            "Mozilla/5.0 (X11; Linux amd64) AppleWebKit/534.36 (KHTML, like Gecko) Chrome/13.0.766.0 Safari/534.36",
            "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/534.36 (KHTML, like Gecko) Chrome/13.0.766.0 Safari/534.36",
            "Mozilla/5.0 (compatible, MSIE 11, Windows NT 6.3; Trident/7.0; rv:11.0) like Gecko",
            "Opera/9.80 (X11; Linux i686; Ubuntu/14.10) Presto/2.12.388 Version/12.16",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36 Edge/12.246",
            "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; AS; rv:11.0) like Gecko",
            "Mozilla/5.0 (Windows; U; MSIE 9.0; WIndows NT 9.0; en-US)",
            "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0; SLCC2; Media Center PC 6.0; InfoPath.3; MS-RTC LM 8; Zune 4.7",
            "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; MS-RTC LM 8; InfoPath.3; .NET4.0C; .NET4.0E) chromeframe/8.0.552.224",
            "Mozilla/5.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; SLCC1; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET CLR 1.1.4322)",
            "Mozilla/4.0 (compatible; MSIE 6.01; Windows NT 6.0)",
            "Opera/9.80 (X11; Linux x86_64; U; en-GB) Presto/2.2.15 Version/10.01",
            "Opera/9.80 (Windows NT 5.1; U; ru) Presto/2.2.15 Version/10.00",
            "Opera/9.64(Windows NT 5.1; U; en) Presto/2.1.1",
            "Opera/10.60 (Windows NT 5.1; U; en-US) Presto/2.6.30 Version/10.60",
            "Opera/9.80 (J2ME/MIDP; Opera Mini/9.80 (J2ME/22.478; U; en) Presto/2.5.25 Version/10.54",
            "Opera/9.80 (Android; Opera Mini/7.6.35766/35.5706; U; en) Presto/2.8.119 Version/11.10",
            "Mozilla/5.0 (Android; Linux armv71; rv:5.0) Gecko/20110615 Fennec/5.0",
            "Mozilla/5.0 (Android; Linux armv7l; rv:9.0) Gecko/20111216 Firefox/9.0 Fennec/9.0",
            "Mozilla/5.0 (Macintosh; U; PPC Mac OS X Mach-O; en-US; rv:1.0.1) Gecko/20021220 Chimera/0.6",
            "Mozilla/5.0 (Windows; U; Windows NT 6.1; rv:2.2) Gecko/20110201",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:25.6) Gecko/20150723 PaleMoon/25.6.0",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:25.6) Gecko/20150723 Firefox/31.9 PaleMoon/25.6.0",
            "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:15.0) Gecko/20120819 Firefox/15.0 PaleMoon/15.0",
            "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.2.11) Gecko/20101023 Firefox/3.6.11 (Palemoon/3.6.11)"
        };

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

        public string SaveTo
        {
            get;
            protected set;
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
            SaveTo = fileName;

            _task = Task.Factory.StartNew(() =>
            {
                _progress.ReportProgress(new ChapterProgress(this, 0));

                string html = DownloadString(Address);

                if (ImageAddresses == null)
                {
                    Console.WriteLine(this.Name);
                    PopulateImageAddress(html);
                }

                string saveToFolder = SaveTo + "\\" + this.Name.RemoveFileNameInvalidChar();
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

                if (UserAgentIndex == -1)
                {
                    UserAgentIndex = (new Random()).Next(0, UserAgents.Length);
                }

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
