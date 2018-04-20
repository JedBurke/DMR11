using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Core.WebsiteHost
{
    public interface IWebiteHostSerializer
    {
        IWebsiteHost SeralizeFromPath(string path);
        IWebsiteHost Serialize(string content);
    }
}
