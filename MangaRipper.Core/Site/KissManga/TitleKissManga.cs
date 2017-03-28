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

            Regex reg = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = reg.Matches(html);


            // Todo: Find a way to get the series name and chapter number as with other hosts.
            // Commenting-out the lines for now.

            /*              
                 // Get the series name.
                 string seriesName = string.Empty;
                 var seriesPattern = "<meta\\s+name=\"description\"\\s+content=\"Read\\s+(.*)(?=\\s+manga\\s+online\\s+free)";
                 var seriesMatch = Regex.Match(html, seriesPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

                 if (seriesMatch.Success && seriesMatch.Groups.Count > 0)
                      seriesName = seriesMatch.Groups[1].Value;
             */


            foreach (Match match in matches)
            {
                var value = new Uri(Address, match.Groups["Value"].Value);
                string name = match.Groups["Text"].Value;
                
                // A variable to store the chapter number.
                // string chapterNumber = string.Empty;

                if (!string.IsNullOrWhiteSpace(name))
                {
                    name = System.Net.WebUtility.HtmlDecode(name);

                    /* 
                        // Get the chapter number.
                        var m = Regex.Match(name, "\\s+(?<chapter>\\d+)(?=\\s+online)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                    
                        if (m.Success)
                        {
                            chapterNumber = m.Groups["chapter"].Value;
                        }
                    
                    
                        if (!string.IsNullOrWhiteSpace(seriesName) && !string.IsNullOrWhiteSpace(chapterNumber))
                        {
                            name = string.Concat(seriesName, " ", chapterNumber).Trim();
                        }
                        else
                        {                    
                            name = Regex.Replace(name, "^Read\\s+|\\s+online$|:", "", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                            name = Regex.Replace(name, "\\s+Read\\s+Online$", "", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                        }
                    */

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
