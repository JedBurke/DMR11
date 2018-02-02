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
    public interface ITitle
    {
        List<IChapter> Chapters
        {
            get;
        }

        UriValidated Address
        {
            get;
        }

        IWebProxy Proxy
        {
            get;
            set;
        }

        Task PopulateChapterAsync(Progress<int> progress);
    }
}
