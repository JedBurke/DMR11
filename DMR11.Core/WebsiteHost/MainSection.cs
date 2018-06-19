using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Core.WebsiteHost
{
    public class MainSection : IMainSection
    {
        public string FriendlyName
        {
            get;
            set;
        }

        public string HostUriPattern
        {
            get;
            set;
        }

        public HostUriType HostUriPatternType
        {
            get;
            set;
        }

        public string RedirectUri
        {
            get;
            set;
        }

        public bool SinglePage
        {
            get;
            set;
        }



        public string ChapterUriPattern
        {
            get;
            set;
        }

        public bool ChapterOnly
        {
            get;
            set;
        }
    }
}
