﻿#define MISSED_KISS

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


namespace DMR11.Core
{
    public abstract class TitleBase : ITitle
    {
        abstract protected string ParseSeriesTitle(string html);

        protected virtual List<UriValidated> ParseChapterAddresses(string html)
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

        public UriValidated Address
        {
            get;
            protected set;
        }

        public IWebProxy Proxy { get; set; }

        public TitleBase(UriValidated address)
        {
            Address = address;
        }

        //abstract string GetSeriesTitle();

        public Task PopulateChapterAsync(Progress<int> progress)
        {
            // Todo: Too long, refactor.

            return Task.Factory.StartNew(() =>
            {
                progress.ReportProgress(0);

#if MISSED_KISS
                var handler = new CloudFlareUtilities.ClearanceHandler(new StatusRedirectionHandler());

                HttpClient client = new HttpClient(handler);
                client.Timeout = TimeSpan.FromSeconds(25);
                string html = null;

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

                    List<UriValidated> addresses = ParseChapterAddresses(html);

                    if (addresses != null)
                    {
                        int count = 0;
                        foreach (Uri item in addresses)
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
