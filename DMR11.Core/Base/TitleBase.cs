#define MISSED_KISS

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using DMR11.Core.WebsiteHost;


namespace DMR11.Core
{
    public abstract class TitleBase : ITitle
    {
        abstract protected string ParseSeriesTitle(string html);

        protected virtual List<Uri> ParseChapterAddresses(string html)
        {
            return null;
        }

        abstract protected List<IChapter> ParseChapterObjects(string html);

        public List<IChapter> Chapters
        {
            get;
            protected set;
        }

        public string SeriesTitle
        {
            get;
            set;
        }

        public Uri Address
        {
            get;
            protected set;
        }

        public IWebProxy Proxy { get; set; }

        public IWebsiteHost HostData { get; set; }

        public Dictionary<string, string> HostVariables { get; set; }

        public TitleBase(Uri address)
        {
            Address = address;
            HostVariables = new Dictionary<string, string>();
        }
        
        public Task PopulateChapterAsync(Progress<int> progress)
        {
            // Todo: Too long, refactor.

            return Task.Factory.StartNew(() =>
            {
                progress.ReportProgress(0);

#if MISSED_KISS
                var handler = new CloudFlareUtilities.ClearanceHandler(new StatusRedirectionHandler(new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
                }));

                HttpClient client = new HttpClient(handler);
                //client.Timeout = TimeSpan.FromSeconds(25);
                
                string html = null;

                client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                client.DefaultRequestHeaders.Add("User-Agent", Service.UserAgent.CurrentUserAgent);     
                
                
                try
                {
                     html = client.GetStringAsync(Address.ToString()).Result;
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }


#else
                var client = new WebClient();
                client.Proxy = Proxy;
                client.Encoding = Encoding.UTF8;
                string html = client.DownloadString(Address);
#endif
                if (!string.IsNullOrWhiteSpace(html))
                {
                    var sb = new StringBuilder();
                    sb.AppendLine(html);

                    SeriesTitle = ParseSeriesTitle(html);

                    List<Uri> addresses = ParseChapterAddresses(html);

                    if (addresses != null)
                    {
                        int count = 0;                        
                        foreach (var item in addresses)
                        {
                            string content = string.Empty;

#if MISSED_KISS
                            content = client.GetStringAsync(item.ToString()).Result;
#else
                        content = client.DownloadString(item);
#endif

                            sb.AppendLine(content);
                            count++;
                            progress.ReportProgress(count * 100 / addresses.Count);
                        }
                    }

                    Chapters = ParseChapterObjects(sb.ToString());
                }

                progress.ReportProgress(100);
            });
        }
    }
}
