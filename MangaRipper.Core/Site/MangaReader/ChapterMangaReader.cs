using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MangaRipper.Core
{
    [Serializable]
    public class ChapterMangaReader : ChapterBase
    {
        public ChapterMangaReader(string name, UriValidated address) : base(name, address) { }

        protected override List<UriValidated> ParseImageAddresses(string html)
        {
            var list = new List<UriValidated>();
            Regex reg = new Regex(@"<img id=""img"" width=""\d+"" height=""\d+"" src=""(?<Value>[^""]+)""",
                RegexOptions.IgnoreCase);
            MatchCollection matches = reg.Matches(html);

            foreach (Match match in matches)
            {
                var value = new UriValidated(Address, match.Groups["Value"].Value);
                list.Add(value);
            }

            return list;
        }

        protected override List<UriValidated> ParsePageAddresses(string html)
        {
            var list = new List<UriValidated>();
            list.Add(Address);

            Regex reg = new Regex(@"<option value=""(?<Value>[^""]+)""(| selected=""selected"")>\d+</option>",
                RegexOptions.IgnoreCase);
            MatchCollection matches = reg.Matches(html);

            foreach (Match match in matches)
            {
                var value = new UriValidated(Address, (match.Groups["Value"].Value));
                if(list.Contains(value) == false)
                {
                    list.Add(value);
                }                
            }

            return list;
        }
    }
}
