using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Core.WebsiteHost
{
    public interface IWebsiteHost
    {
        IMetaSection Meta { get; }

        IMainSection Host { get; }

        HostSection Title { get; }

        HostSection Chapters { get; }

        HostSection Pages { get; }

        HostSection Page { get; }

    }
}
