using MangaRipper.Core.Helper;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MangaRipper.Core
{
    public class TitleMangaFox : TitleBase
    {
        public TitleMangaFox(UriValidated address) : base(address) { }

        protected override List<IChapter> ParseChapterObjects(string html)
        {
            var details = new ChapterParseDetails("//a[contains(@class, 'tips')]", "href", ChapterParseAction);
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
