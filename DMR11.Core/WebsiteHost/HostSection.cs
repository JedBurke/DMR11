using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Core.WebsiteHost
{
    public class HostSection : IHostSection
    {
        public string Path { get; set; }
        public string Value { get; set; }

        public string ParseRegex { get; set; }
        public string ParseReplace { get; set; }

        public HostSection()
        {
        }

        public HostSection(string path, string value, string parseRegexPattern = null, string parseReplace = null)
        {
            this.Path = path;
            this.Value = value;

            this.ParseRegex = parseRegexPattern;
            this.ParseReplace = parseReplace;
        }
    }
}
