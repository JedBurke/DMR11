﻿using HtmlAgilityPack;
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
            var list = new List<UriValidated>();
            var imgXpath = "//img[@id='image']";

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var images = doc.DocumentNode.SelectNodes(imgXpath);
            foreach (var image in images)
            {
                var imageSource = image.GetAttributeValue("src", string.Empty);

                if (!string.IsNullOrWhiteSpace(imageSource))
                {
                    list.Add(new UriValidated(imageSource));
                }
            }
            
            logger.Trace("Images found: {0}", list.Count);

            return list;
        }

        protected override List<UriValidated> ParsePageAddresses(string html)
        {
            var list = new List<UriValidated>();
            var pageAddressXpath = "//select[@class='m'][1]/option";
            var doc = new HtmlDocument();

            doc.LoadHtml(html);
            var pages = doc.DocumentNode.SelectNodes(pageAddressXpath);

            if (pages != null)
            {
                foreach (var page in pages)
                {
                    var pageNumberValue = page.GetAttributeValue("value", string.Empty);
                    int pageNumberInt = 0;

                    if (!string.IsNullOrWhiteSpace(pageNumberValue) && int.TryParse(pageNumberValue, out pageNumberInt))
                    {
                        list.Add(new UriValidated(Address, string.Concat(pageNumberValue, ".html")));
                    }                    
                }
            }


            return list.Distinct().ToList();
        }
    }
}
