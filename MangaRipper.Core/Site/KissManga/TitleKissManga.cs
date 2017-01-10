using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MangaRipper.Core
{
    class TitleKissManga : TitleBase
    {
        public TitleKissManga(Uri address) : base(address) { }

        protected override List<IChapter> ParseChapterObjects(string html)
        {
            var list = new List<IChapter>();
            string pattern = "<td>\n<a href=\"(?=/Manga/)(?<Value>.[^\"]*)\" title=\"(?<Text>.[^\"]*)\"";
            
            Regex reg = new Regex(pattern,
                RegexOptions.IgnoreCase);
            MatchCollection matches = reg.Matches(html);

            foreach (Match match in matches)
            {
                var value = new Uri(Address, match.Groups["Value"].Value);                
                string name = match.Groups["Text"].Value;

                if (!string.IsNullOrWhiteSpace(name))
                {
                    name = System.Net.WebUtility.HtmlDecode(name);
                    name = Regex.Replace(name, "^Read\\s+|\\s+online$|:", "", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                    name = Regex.Replace(name, "\\s+Read\\s+Online$", "", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                }

                Console.WriteLine(name);

                IChapter chapter = new ChapterKissManga(name, value);
                list.Add(chapter);

            }
            
            return list;
        }

        protected override List<Uri> ParseChapterAddresses(string html)
        {
            return base.ParseChapterAddresses(html);
        }
    }
}
