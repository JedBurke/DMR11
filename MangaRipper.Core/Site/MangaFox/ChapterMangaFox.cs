using MangaRipper.Core.Helper;
using HtmlAgilityPack;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MangaRipper.Core
{
    [Serializable]
    public class ChapterMangaFox : ChapterBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ChapterMangaFox(string name, UriValidated address) : base(name, address) { }

        protected override List<UriValidated> ParseImageAddresses(string html)
        {
            var details = new ParseDetails<UriValidated>
            (
                "//img[@id='image']",
                "src",
                (element, parseDetails) => Parsing.CreateUriFromElementAttributeValue(element, parseDetails),
                logger
            );

            return Parsing.ParseAddresses(html, details);
        }

        protected override List<UriValidated> ParsePageAddresses(string html)
        {
            var details = new ParseDetails<UriValidated>(
                "//select[@class='m'][1]/option",
                "value",
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
