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


namespace MangaRipper.Core
{
    public abstract class TitleBase : ITitle
    {

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

        public Uri Address
        {
            get;
            protected set;
        }

        public IWebProxy Proxy { get; set; }

        public TitleBase(Uri address)
        {
            Address = address;
        }

        public Task PopulateChapterAsync(Progress<int> progress)
        {
            return Task.Factory.StartNew(() =>
            {
                progress.ReportProgress(0);

#if MISSED_KISS
                CloudFlareUtilities.ClearanceHandler handler = new CloudFlareUtilities.ClearanceHandler();

                HttpClient client = new HttpClient(handler);
                client.Timeout = TimeSpan.FromSeconds(25);
                string html = null;

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
                var sb = new StringBuilder();
                sb.AppendLine(html);

                List<Uri> uris = ParseChapterAddresses(html);

                if (uris != null)
                {
                    int count = 0;
                    foreach (Uri item in uris)
                    {
                        string content = string.Empty;

#if MISSED_KISS
                        content = client.GetStringAsync(item.ToString()).Result;
#else
                        content = client.DownloadString(item);
#endif

                        sb.AppendLine(content);
                        count++;
                        progress.ReportProgress(count * 100 / uris.Count);
                    }
                }

                Chapters = ParseChapterObjects(sb.ToString());

                progress.ReportProgress(100);
            });
        }
    }
}
