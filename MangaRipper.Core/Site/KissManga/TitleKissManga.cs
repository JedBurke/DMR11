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
            string pattern = "<td>\\s+<a\\s+href=\"(?=/Manga/)(?<Value>.[^\"]*)\"\\s+title=\"(?<Text>.[^\"]*)\"";

            Regex reg = new Regex(pattern,
                RegexOptions.IgnoreCase);
            MatchCollection matches = reg.Matches(html);

            //string seriesName = string.Empty;

            //var seriesMatch = Regex.Match(html, "<title>\\s*(<series>.*)(?=\\s+manga)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            
            //foreach (Group i in seriesMatch.Groups)
            //{
            //    Console.WriteLine(i.Value);
            //}


            foreach (Match match in matches)
            {
                var value = new Uri(Address, match.Groups["Value"].Value);
                string name = match.Groups["Text"].Value;
                string chapterNumber = string.Empty;

                if (!string.IsNullOrWhiteSpace(name))
                {
                    name = System.Net.WebUtility.HtmlDecode(name);

                    var m = Regex.Match(name, "-0*(?<chapter>\\d+)(?=\\?)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                    if (m.Success)
                    {
                        chapterNumber = m.Groups["chapter"].Value;
                    }

                    name = Regex.Replace(name, "^Read\\s+|\\s+online$|:", "", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                    name = Regex.Replace(name, "\\s+Read\\s+Online$", "", RegexOptions.IgnoreCase | RegexOptions.Compiled);

                }


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
