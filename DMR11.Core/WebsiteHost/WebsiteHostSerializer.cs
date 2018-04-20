using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Core.WebsiteHost
{
    public abstract class WebsiteHostSerializer : IWebiteHostSerializer
    {
        public IWebsiteHost SeralizeFromPath(string path)
        {
            string content = string.Empty;

            if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
            {
                content = File.ReadAllText(path);

                return Serialize(content);
            }

            return null;
        }

        public abstract IWebsiteHost Serialize(string content);
    }
}
