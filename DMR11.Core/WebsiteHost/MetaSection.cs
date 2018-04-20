using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Core.WebsiteHost
{
    public class MetaSection : IMetaSection
    {

        public string HostUriPattern
        {
            get;
            set;
        }

        public HostType HostType
        {
            get;
            set;
        }

        public string ScriptPath
        {
            get;
            set;
        }

        public Version MaximumVersion
        {
            get;
            set;
        }

        public Version MinimumVersion
        {
            get;
            set;
        }
    }
}
