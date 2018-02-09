using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DMR11.Core
{
    public interface IChapter
    {
        string Name
        {
            get;
            set;
        }

        UriValidated Address
        {
            get;
        }

        string SaveDestination
        {
            get;
            set;
        }

        bool IsBusy
        {
            get;
        }

        IWebProxy Proxy
        {
            get;
            set;
        }

        Task DownloadImageAsync(string fileName, CancellationToken cancellationToken, Progress<ChapterProgress> progress);
    }
}
