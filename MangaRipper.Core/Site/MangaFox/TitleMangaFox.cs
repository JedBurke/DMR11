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
            var details = new ParseDetails<IChapter>
            {
                xpath = "//a[contains(@class, 'tips')]",
                attributeName = "href",
                
                parseAction = new Func<HtmlNode, ParseDetails<IChapter>, IChapter>((element, parseDetails) => 
                {
                    var uri = Parsing.CreateUriFromElementAttributeValue(element, parseDetails);
                    var chapter = new ChapterMangaFox(element.InnerText, uri);

                    return chapter ?? null;
                })
            };

            return Parsing.ParseChapters(html, details);
        }
    }
}
