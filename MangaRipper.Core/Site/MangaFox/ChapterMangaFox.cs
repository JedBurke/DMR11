﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MangaRipper.Core
{
    [Serializable]
    public class ChapterMangaFox : ChapterBase
    {
        public ChapterMangaFox(string name, Uri address) : base(name, address) { }

        protected override List<Uri> ParseImageAddresses(string html)
        {
            var list = new List<Uri>();

            string imageRegexPattern = "<img\\s+src=\"(?<Value>[^\"]+)\"\\s+width";

            Regex reg = new Regex(imageRegexPattern,
                                  RegexOptions.IgnoreCase);
            MatchCollection matches = reg.Matches(html);

            foreach (Match match in matches)
            {
                var value = new Uri(Address, match.Groups["Value"].Value);
                list.Add(value);
            }

            Console.WriteLine("MangaFox: {0} pages", list.Count);

            return list;
        }

        protected override List<Uri> ParsePageAddresses(string html)
        {
            var list = new List<Uri>();
            Regex reg = new Regex(@"<option value=""(?<Value>[^""]+)"" (|selected=""selected"")>\d+</option>", RegexOptions.IgnoreCase);
            MatchCollection matches = reg.Matches(html);

            foreach (Match match in matches)
            {
                var value = new Uri(Address, (match.Groups["Value"].Value + ".html"));
                list.Add(value);
            }
            
            return list.Distinct().ToList();
        }
    }
}
