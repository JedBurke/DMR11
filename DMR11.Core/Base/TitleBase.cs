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
using NLog;


namespace DMR11.Core
{
    public abstract class TitleBase : ITitle
    {
        static ILogger _log;
        protected static ILogger Log
        {
            get
            {
                return _log ?? (_log = LogManager.GetCurrentClassLogger());
            }
        }

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

        public bool IsChapter
        {
            get;
            set;
        }

        public IWebProxy Proxy { get; set; }

        public IWebsiteHost HostData { get; set; }

        public Dictionary<string, string> HostVariables { get; set; }

        public TitleBase(Uri address, ILogger log)
        {
            Address = address;
            HostVariables = new Dictionary<string, string>();

            _log = log;

            Log.Trace("URI: {0}", address.ToString());
        }
        
        public Task PopulateChapterAsync(Progress<int> progress)
        {
            // Todo: Too long, refactor.

            return Task.Factory.StartNew(() =>
            {
                progress.ReportProgress(0);

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

                Log.Trace("Entering PopulateChapterAsync");
                Log.Trace("URI: {0}", Address.ToString());

                Log.Trace("Setting client default request headers");
                Log.Trace("Accept: {0}", client.DefaultRequestHeaders.Accept);
                Log.Trace("Accept-Encoding: {0}", client.DefaultRequestHeaders.AcceptEncoding);
                Log.Trace("User-Agent: {0}", client.DefaultRequestHeaders.UserAgent);
                
                try
                {
                     html = client.GetStringAsync(Address.ToString()).Result;
                    
                }
                catch (Exception ex)
                {
                    Log.Error(ex.ToString());
                    Console.WriteLine(ex.ToString());                    
                }
                
                if (!string.IsNullOrWhiteSpace(html))
                {
                    var sb = new StringBuilder();
                    sb.AppendLine(html);

                    SeriesTitle = ParseSeriesTitle(html);
                    Log.Trace("Series title: {0}", SeriesTitle);

                    List<Uri> addresses = ParseChapterAddresses(html);

                    if (IsChapter)
                    {
                        Log.Debug("Single chapter address");                        
                        sb.AppendLine(html);
                    }
                    else if (addresses != null)
                    {
                        Log.Debug("Multi-chapter series");

                        int count = 0;

                        foreach (var address in addresses)
                        {
                            string content = string.Empty;

                            content = client.GetStringAsync(address.ToString()).Result;

                            sb.AppendLine(content);
                            count++;
                            progress.ReportProgress(count * 100 / addresses.Count);
                        }
                    }
                    else
                    {
                        Log.Debug("No addresses found");
                    }

                    Chapters = ParseChapterObjects(sb.ToString());
                }

                progress.ReportProgress(100);
            });
        }
    }
}
