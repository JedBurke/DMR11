using DMR11.Core.WebsiteHost;
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

        private IWebsiteHost HostData;

        public ChapterDistill(string name, UriValidated address, IWebsiteHost hostData)
            : base(name, address)
        {

            if (hostData == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                HostData = hostData;

                //Referrer = HostData["host"]["referer"];

                HostVariables.Add("chapter", Name);
                                
                // Short-circuit the page listing if all of the 'pages' (chapter images) are in a single HTML page.
                SinglePage = HostData.Host.SinglePage;
                
            }
        }

        protected override List<UriValidated> ParseImageAddresses(string html)
        {
            var details = new ParseDetails<UriValidated>
            (
                HostData.Page.Path,
                HostData.Page.Value,
                (element, parseDetails) => Parsing.CreateUriFromElementAttributeValue(element, parseDetails, new UriValidated(HostVariables["address_trimmed"])),
                logger
            );

            return Parsing.ParseAddresses(html, details);
        }

        protected override List<UriValidated> ParsePageAddresses(string html)
        {
            if (SinglePage)
            {
                return new List<UriValidated>();
            }
            else
            {
                var details = new ParseDetails<UriValidated>(
                    HostData.Pages.Path,
                    HostData.Pages.Value,
                    (element, parseDetails) => GenericParseAction(element, parseDetails, HostData.Pages, (uri) => new UriValidated(uri)),
                    logger
                );
                
                return Parsing.ParseAddresses(html, details);
            }
        }

        private T GenericParseAction<T>(HtmlNode element, IParseDetails<T> details, IHostSection section, Func<string, T> postParse)
        {
            if (!string.IsNullOrWhiteSpace(section.ParseRegex) &&
                !string.IsNullOrWhiteSpace(section.ParseReplace))
            {
                var regex = new Regex(section.ParseRegex);
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

                    var replace = VariableLookup(HostData.Pages.ParseReplace);
                    return postParse(replace);
                }
            }
            else
            {
                var input = element.GetAttributeValue(details.AttributeName, string.Empty);
                return postParse(input);
            }

            return default(T);
        }

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

            if (!string.IsNullOrWhiteSpace(HostData.Pages.ParseRegex) &&
                !string.IsNullOrWhiteSpace(HostData.Pages.ParseReplace))
            {
                var regex = new Regex(HostData.Pages.ParseRegex);
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

                    var replace = VariableLookup(HostData.Pages.ParseReplace);
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

        //Dictionary<string, string> HostVariables = null;

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
