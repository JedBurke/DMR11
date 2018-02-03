﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DMR11.Core
{
    [Serializable]
    class ChapterMangaHere : ChapterBase
    {
        public ChapterMangaHere(string name, UriValidated address) : base(name, address) { }

        protected override List<UriValidated> ParsePageAddresses(string html)
        {
            var list = new List<UriValidated>();
            list.Add(Address);
            Regex reg = new Regex(@"<option value=""(?<Value>http://www.mangahere.co[m]?/manga/[^""]+)"" (|selected=""selected"")>\d+</option>",
                RegexOptions.IgnoreCase);
            MatchCollection matches = reg.Matches(html);

            foreach (Match match in matches)
            {
                var value = new UriValidated(Address, match.Groups["Value"].Value);

                if (!list.Contains(value))
                    list.Add(value);
            }

            return list.Distinct().ToList();
        }

        protected override List<UriValidated> ParseImageAddresses(string html)
        {
            var list = new List<UriValidated>();

            // "<img src=\"(?<Value>[^\"]+)\" onerror=\""
            string pattern = "<img src=\"(?<Value>[^\"]+)\" onload=\"loadImg\\(this\\)\"";
            
            Regex reg = new Regex(
                pattern,
                RegexOptions.IgnoreCase);

            MatchCollection matches = reg.Matches(html);

            foreach (Match match in matches)
            {
                var value = new UriValidated(Address, match.Groups["Value"].Value);
                list.Add(value);
            }

            /*List<string> s = new List<string>();
            s.Add("Image Links: " + DateTime.Now.ToShortTimeString());

            foreach (Uri u in list)
                s.Add(u.ToString());

            System.IO.File.WriteAllLines(@"D:\rn.txt", s.ToArray());*/

            return list;
        }
    }
}