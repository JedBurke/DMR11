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

            return websiteHost;
        }

        public IniData ParseWebsiteHostFile(string content)
        {
            var parser = new IniParser.Parser.IniDataParser();
            var websiteHostFile = parser.Parse(content);

            return websiteHostFile;
        }

        public IWebsiteHost SerializeProperties(IniData websiteHostFile)
        {
            const string
                SECTION_META = "meta",
                SECTION_HOST = "host",
                SECTION_TITLE = "title",
                SECTION_CHAPTERS = "chapters",
                SECTION_PAGES = "pages",
                SECTION_PAGE = "page";

            var websiteHost = new WebsiteHost();

            bool hostSinglePage = false;
            
            bool.TryParse(websiteHostFile[SECTION_HOST]["single_page"], out hostSinglePage);

            websiteHost.Meta.HostType = HostType.Simple;
            websiteHost.Meta.ScriptPath = websiteHostFile[SECTION_META]["script"];
            websiteHost.Meta.MinimumVersion = Version.Parse("0.5.0");
            websiteHost.Meta.MaximumVersion = Version.Parse("0.5.0");

            websiteHost.Host.FriendlyName = websiteHostFile[SECTION_HOST]["friendly_name"];
            websiteHost.Host.HostUriPattern = websiteHostFile[SECTION_HOST]["uri"];
            websiteHost.Host.HostUriPatternType = HostUriType.Simple;
            websiteHost.Host.RedirectUri = websiteHostFile[SECTION_HOST]["redirect_uri"];                        
            websiteHost.Host.SinglePage = hostSinglePage;

            websiteHost.Title.Path = websiteHostFile[SECTION_TITLE]["path"];
            websiteHost.Title.Value = websiteHostFile[SECTION_TITLE]["value"];
            websiteHost.Title.ParseRegex = websiteHostFile[SECTION_TITLE]["parse_regex"];
            websiteHost.Title.ParseReplace = websiteHostFile[SECTION_TITLE]["parse_replace"];
            
            websiteHost.Chapters.Path = websiteHostFile[SECTION_CHAPTERS]["path"];
            websiteHost.Chapters.Value = websiteHostFile[SECTION_CHAPTERS]["value"];
            websiteHost.Chapters.ParseRegex = websiteHostFile[SECTION_CHAPTERS]["parse_regex"];
            websiteHost.Chapters.ParseReplace = websiteHostFile[SECTION_CHAPTERS]["parse_replace"];
            
            websiteHost.Pages.Path = websiteHostFile[SECTION_PAGES]["path"];
            websiteHost.Pages.Value = websiteHostFile[SECTION_PAGES]["value"];
            websiteHost.Pages.ParseRegex = websiteHostFile[SECTION_PAGES]["parse_regex"];
            websiteHost.Pages.ParseReplace = websiteHostFile[SECTION_PAGES]["parse_replace"];

            websiteHost.Page.Path = websiteHostFile[SECTION_PAGE]["path"];
            websiteHost.Page.Value = websiteHostFile[SECTION_PAGE]["value"];
            websiteHost.Page.ParseRegex = websiteHostFile[SECTION_PAGE]["parse_regex"];
            websiteHost.Page.ParseReplace = websiteHostFile[SECTION_PAGE]["parse_replace"];

            return websiteHost;
        }

    }
}
