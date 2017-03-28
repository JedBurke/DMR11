using HtmlAgilityPack;
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

        ScriptEngine Engine = null;

        public override void PreParseImageAddresses(params object[] options)
        {
            if (options.Length != 1)
                return;

            var doc = new HtmlDocument();
            doc.LoadHtml(options[0].ToString());
            
            // Todo: Replace with live versions.
            Engine.Execute(Properties.Resources.KissManga_CryptoJs);
            Engine.Execute(Properties.Resources.KissManga_lo);
                        
            // Finds and executes each script element which mentions CryptoJS.
            foreach (var node in doc.DocumentNode.SelectNodes("//script"))
            {
                if (!string.IsNullOrWhiteSpace(node.InnerText) && node.InnerText.IndexOf("CryptoJS", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    Engine.Execute(node.InnerText);
                }
            }

            doc = null;
        }
        
        string SanitizeImageAddress(string imageAddress, ScriptEngine engine, string decryptionFunction)
        {
            try
            {
                return engine.CallGlobalFunction<string>(decryptionFunction, imageAddress);
            }
            catch (Exception)
            {
                return imageAddress;
            }
            
        }

        protected override List<Uri> ParseImageAddresses(string html)
        {
            if (Engine == null)
                Engine = new Jurassic.ScriptEngine();

            PreParseImageAddresses(html);

            var list = new List<Uri>();

            string imageRegexPattern = "lstImages.push\\((?<Func>.[^\\(]*)\\(\"(?<Value>.[^\"]*)\"\\)";

            Regex reg = new Regex(imageRegexPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            MatchCollection matches = reg.Matches(html);

            if (matches != null && matches.Count > 0)
            {                                
                foreach (Match match in matches)
                {
                    // Capture the name of the decryption function in the evnt it changes.
                    string decryptFunctionName = match.Groups["Func"].Value;

                    string uri = SanitizeImageAddress(match.Groups["Value"].Value, Engine, decryptFunctionName);

                    var value = new Uri(Address, uri);
                    list.Add(value);
                }                
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
