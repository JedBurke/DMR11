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
    public class TitleMangaFox : TitleBase
    {
        static ILogger logger = LogManager.GetCurrentClassLogger();

        public TitleMangaFox(UriValidated address) : base(address) { }

        protected override List<IChapter> ParseChapterObjects(string html)
        {
            logger.Debug("Entering ParseChapterObjects");
            logger.Trace("Html parameter: {0}", html);

            var details = new ChapterParseDetails("//a[contains(@class, 'tips')]", "href", ChapterParseAction, logger);
            return Parsing.ParseChapters(html, details);
        }

        public IChapter ChapterParseAction(HtmlNode element, IParseDetails<IChapter> parseDetails)
        {
            var uri = Parsing.CreateUriFromElementAttributeValue(element, parseDetails);
            var chapter = new ChapterMangaFox(element.InnerText, uri);

            return chapter ?? null;
        }
    }
}
