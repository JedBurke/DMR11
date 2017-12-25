using IniParser.Model;
using IniParser.Parser;
using HtmlAgilityPack;
using DMR11.Core.Helper;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Core
{
    public class ChapterDistill : ChapterBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IniData HostData = null;

        public ChapterDistill(string name, UriValidated address, IniData configData) : base(name, address) {
            
            if (configData == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                HostData = configData;

                //Referrer = HostData["host"]["referer"];
            }
        }

        protected override List<UriValidated> ParseImageAddresses(string html)
        {
            string path = HostData["page"]["path"],
                   value = HostData["page"]["value"];

            var details = new ParseDetails<UriValidated>
            (
                path,
                value,
                (element, parseDetails) => Parsing.CreateUriFromElementAttributeValue(element, parseDetails),
                logger
            );

            return Parsing.ParseAddresses(html, details);
        }

        protected override List<UriValidated> ParsePageAddresses(string html)
        {
            string path = HostData["pages"]["path"],
                   value = HostData["pages"]["value"];

            var details = new ParseDetails<UriValidated>(
                path,
                value,
                (element, parseDetails) => ParseAction(element, parseDetails),
                logger
            );
            
            return Parsing.ParseAddresses(html, details);

        }

        private UriValidated ParseAction(HtmlNode element, IParseDetails<UriValidated> details)
        {
            var pageNumberValue = element.GetAttributeValue(details.AttributeName, string.Empty);
            var pageNumberInt = 0;

            if (!string.IsNullOrWhiteSpace(pageNumberValue) && int.TryParse(pageNumberValue, out pageNumberInt))
            {
                details.Logger.Trace("Parsing chapter link as \"{0}\"", string.Concat(Address.ToString(), pageNumberInt, ".html"));
                return new UriValidated(Address, string.Concat(pageNumberInt, ".html"));
            }

            return null;
        }
    }
}
