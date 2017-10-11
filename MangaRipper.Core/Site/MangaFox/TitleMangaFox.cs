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
            var list = new List<IChapter>();
            var chapterXpath = "//a[contains(@class, 'tips')]";
            var doc = new HtmlDocument();
            
            doc.LoadHtml(html);
            var chapterElements = doc.DocumentNode.SelectNodes(chapterXpath);
            foreach (var chapterElement in chapterElements)
            {
                UriValidated uri = new UriValidated(chapterElement.GetAttributeValue("href", null));
                IChapter chapter = new ChapterMangaFox(chapterElement.InnerText, uri);

                list.Add(chapter);
            }

            return list;
        }
    }
}
