using Jurassic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace MangaRipper.Core
{
    [Serializable]
    class ChapterKissManga : ChapterBase
    {
        public ChapterKissManga(string name, Uri address)
            : base(name, address)
        {
            SinglePage = true;
        }

        protected override List<Uri> ParseImageAddresses(string html)
        {

            var list = new List<Uri>();

            string imageRegexPattern = "lstImages.push\\((?<Func>.[^\\(]*)\\(\"(?<Value>.[^\"]*)\"\\)";

            Regex reg = new Regex(imageRegexPattern,
                                  RegexOptions.IgnoreCase | RegexOptions.Compiled);

            MatchCollection matches = reg.Matches(html);

            if (matches != null && matches.Count > 0)
            {
                var engine = new Jurassic.ScriptEngine();
                engine.Execute(Properties.Resources.KissManga_CryptoJs);
                engine.Execute(Properties.Resources.KissManga_lo);
                
                foreach (Match match in matches)
                {
                    string function = match.Groups["Func"].Value;
                    
                    string decryptedUri = engine.CallGlobalFunction<string>(function, match.Groups["Value"].Value);

                    var value = new Uri(Address, decryptedUri);
                    list.Add(value);
                }
                
                engine = null;
            }

            return list;
        }


        protected override List<Uri> ParsePageAddresses(string html)
        {
            List<Uri> list = new List<Uri>();

            string pattern = "<option value=\"(Ch-.[^\"]*)\" selected";
            Regex reg = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            MatchCollection matches = reg.Matches(html);

            foreach (Match match in matches)
            {
                var value = new Uri(Address, (match.Groups["Value"].Value));

                Console.WriteLine(value);
                if (!list.Contains(value))
                    list.Add(value);
            }

            return list.Distinct().ToList();
        }
    }
}
