using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using DMR11.Core.WebsiteHost;

namespace DMR11.Core
{
    public interface ITitle
    {
        string SeriesTitle
        {
            get;
            set;
        }

        List<IChapter> Chapters
        {
            get;
        }

        Uri Address
        {
            get;
        }

        IWebProxy Proxy
        {
            get;
            set;
        }

        IWebsiteHost HostData
        {
            get;
            set;
        }

        Dictionary<string, string> HostVariables
        {
            get;
            set;
        }

        bool IsChapter
        {
            get;
            set;
        }
        
        Task PopulateChapterAsync(Progress<double> progress);
    }
}
