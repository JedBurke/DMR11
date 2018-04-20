using IniParser.Model;
using IniParser.Parser;
using HtmlAgilityPack;
using DMR11.Core.Helper;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DMR11.Core
{
    public class ChapterDistill : ChapterBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IniData HostData = null;

        public ChapterDistill(string name, UriValidated address, IniData configData)
            : base(name, address)
        {

            if (configData == null)
            {
                throw new ArgumentNullException();
            }
            else
            {

                HostVariables = new Dictionary<string, string>();
                HostVariables.Add("host", address.Host);
                HostVariables.Add("address", address.ToString());
                HostVariables.Add("address_trimmed", address.ToString().Substring(0, address.ToString().LastIndexOf('/')));

                HostData = configData;

                //Referrer = HostData["host"]["referer"];

                // Short-circuit the page listing if all of the 'pages' (chapter images) are in a single HTML page.
                if (HostData["host"].ContainsKey("single_page"))
                {
                    bool singlePage = false;
                    bool.TryParse(HostData["host"]["single_page"], out singlePage);

                    SinglePage = singlePage;

                }
                
            }
        }

        protected override List<UriValidated> ParseImageAddresses(string html)
        {
            string path = HostData["page"]["path"],
                   value = HostData["page"]["value"];

            var details = new ParseDetails<UriValidated>
            (
                path,
                value,
                (element, parseDetails) => Parsing.CreateUriFromElementAttributeValue(element, parseDetails, new UriValidated(HostVariables["address_trimmed"])),
                logger
            );

            return Parsing.ParseAddresses(html, details);
        }

        protected override List<UriValidated> ParsePageAddresses(string html)
        {
            if (SinglePage)
                return new List<UriValidated>();

            string path = HostData["pages"]["path"],
                   value = HostData["pages"]["value"];

            var details = new ParseDetails<UriValidated>(
                path,
                value,
                (element, parseDetails) => ParseAction(element, parseDetails),
                logger
            );

            return Parsing.ParseAddresses(html, details);

        }

        static readonly string
            HOST_SECTION_PAGES = "pages",
            HOST_KEY_PARSE_REGEX = "parse_regex",
            HOST_KEY_PARSE_REPLACE = "parse_replace";

        private UriValidated ParseAction(HtmlNode element, IParseDetails<UriValidated> details)
        {
            // If there is a function registered and it returns the address, parse it, otherwise return null.

            //var pageNumberValue = element.GetAttributeValue(details.AttributeName, string.Empty);
            //var pageNumberInt = 0;

            //if (!string.IsNullOrWhiteSpace(pageNumberValue) && int.TryParse(pageNumberValue, out pageNumberInt))
            //{
            //    details.Logger.Trace("Parsing chapter link as \"{0}\"", string.Concat(Address.ToString(), pageNumberInt, ".html"));
            //    return new UriValidated(Address, string.Concat(pageNumberInt, ".html"));
            //}

            if (
                HostData[HOST_SECTION_PAGES].ContainsKey(HOST_KEY_PARSE_REGEX) &&
                HostData[HOST_SECTION_PAGES].ContainsKey(HOST_KEY_PARSE_REPLACE)
                )
            {
                var regex = new Regex(HostData[HOST_SECTION_PAGES][HOST_KEY_PARSE_REGEX]);
                var match = Match.Empty;

                var input = element.GetAttributeValue(details.AttributeName, string.Empty);
                if ((match = regex.Match(input)).Success)
                {
                    // Register group values.
                    foreach (var group in regex.GetGroupNames())
                    {
                        var newKey = string.Concat("regex__", group);
                        var newValue = match.Groups[group].Value;

                        if (HostVariables.ContainsKey(newKey))
                        {
                            HostVariables[newKey] = newValue;
                        }
                        else
                        {
                            HostVariables.Add(newKey, newValue);
                        }
                    }

                    //input = VariableLookup(input);

                    //return new UriValidated(input);

                    var replace = VariableLookup(HostData[HOST_SECTION_PAGES][HOST_KEY_PARSE_REPLACE]);
                    return new UriValidated(replace);
                }
            }
            else
            {
                var input = element.GetAttributeValue(details.AttributeName, string.Empty);
                return new UriValidated(input);
            }

            return null;
        }

        Dictionary<string, string> HostVariables = null;

        private string VariableLookup(string input)
        {
            if (HostVariables != null && !string.IsNullOrWhiteSpace(input))
            {
                var lookup = Regex.Replace(
                    input,
                    @"\$\((.[^\)]*)\)",
                    new MatchEvaluator((Match e) =>
                    {
                        if (e.Success)
                        {
                            var key = e.Groups[1].Value;
                            if (HostVariables.ContainsKey(key))
                            {
                                return HostVariables[key];
                            }
                        }

                        return input;
                    })
               );

                return lookup;
            }

            return input;
        }

    }
}
