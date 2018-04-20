using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IniParser.Parser;
using IniParser.Model;

namespace DMR11.Core.WebsiteHost
{
    public class IniWebsiteHostSerializer : WebsiteHostSerializer
    {
        public override IWebsiteHost Serialize(string content)
        {
            var websiteHostFile = ParseWebsiteHostFile(content);
            var websiteHost = SerializeProperties(websiteHostFile);

            return new WebsiteHost();
        }

        public IniParser.Model.IniData ParseWebsiteHostFile(string content)
        {
            var parser = new IniParser.Parser.IniDataParser();
            var websiteHostFile = parser.Parse(content);

            return websiteHostFile;
        }

        public IWebsiteHost SerializeProperties(IniData websiteHostFile)
        {
            var websiteHost = new WebsiteHost();

            bool hostSinglePage = false;


            bool.TryParse(websiteHostFile["host"]["single_page"], out hostSinglePage);

            websiteHost.Meta.HostUriPattern = websiteHostFile["host"]["uri"];
            websiteHost.Host.FriendlyName = websiteHostFile["host"]["friendly_name"];
            websiteHost.Host.RedirectUri = websiteHostFile["host"]["redirect_uri"];
                        
            websiteHost.Host.SinglePage = hostSinglePage;

            // Disregard the option for now.
            websiteHost.Host.HostUriPatternType = HostUriType.Simple;

            return websiteHost;
        }

    }
}
