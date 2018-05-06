using NLog;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using DMR11.Core.WebsiteHost;

namespace DMR11.Core.Helper
{
    public class Parsing
    {
        public static List<UriValidated> ParseAddresses(string html, IParseDetails<UriValidated> details)
        {
            return ParseContent<UriValidated>(html, details);
        }

        public static List<IChapter> ParseChapters(string html, IParseDetails<IChapter> details)
        {
            return ParseContent<IChapter>(html, details);
        }

        public static List<T> ParseContent<T>(string html, IParseDetails<T> details)
        {
            var list = new List<T>();
            var doc = new HtmlDocument();

            if (string.IsNullOrWhiteSpace(html))
                return list;

            try
            {
                doc.LoadHtml(html);

                var elements = doc.DocumentNode.SelectNodes(details.XPath);

                if (elements != null && elements.Count > 0)
                {
                    details.Logger.Debug("Selected node count: {0}", elements.Count);

                    foreach (var element in elements)
                    {
                        //Console.WriteLine("Node hit: {0}", element.InnerText);

                        T obj = default(T);

                        if (details.ParseAction != null)
                        {
                            obj = details.ParseAction(element, details);

                            if (obj == null)
                            {
                                throw new ArgumentNullException();
                            }
                        }
                        else
                        {
                            throw new ArgumentNullException();
                        }

                        list.Add(obj);

                    }
                }
            }
            finally
            {
                doc = null;
            }

            return list.Distinct().ToList();
        }

        public static UriValidated CreateUriFromElementAttributeValue<T>(HtmlNode element, IParseDetails<T> details, UriValidated host) 
        {
            var value = element.GetAttributeValue(details.AttributeName, null);
            details.Logger.Debug("Creating validated URI from \"{0}\"", value);

            if (value.StartsWith("/") && value[1] != '/')
            {
                value = string.Concat(host.Scheme, "://", host.Host, value);
            }

            return new UriValidated(value);
        }

        private static string GetElementValue(HtmlNode element, string innerSelector)
        {
            string input = string.Empty;
            
            if (string.Compare(innerSelector, "$(__inner_text)", true) == 0)
            {
                input = element.InnerText;
            }
            else
            {
                input = element.GetAttributeValue(innerSelector, string.Empty);
            }
            
            return input;
        }

        public static T GenericParseAction<T>(HtmlNode element, IParseDetails<T> details, IHostSection section, Func<string, T> postParse, Dictionary<string, string> hostVariables)
        {
            var input = GetElementValue(element, details.AttributeName);
            
            // Remove any trailing whitespace or newlines from the value.
            input = input.Trim();

            if (!string.IsNullOrWhiteSpace(section.ParseRegex) &&
                !string.IsNullOrWhiteSpace(section.ParseReplace))
            {
                var regex = new Regex(section.ParseRegex);
                var match = Match.Empty;
                
                if ((match = regex.Match(input)).Success)
                {
                    // Register group values.
                    foreach (var group in regex.GetGroupNames())
                    {
                        // Todo: Use section name as well before overwriting the original value.
                        // > regex__pages_conflicting_name
                        // > regex__page_conflicting_name

                        var newKey = string.Concat("regex__", group);
                        var newValue = match.Groups[group].Value;

                        if (hostVariables.ContainsKey(newKey))
                        {
                            hostVariables[newKey] = newValue;
                        }
                        else
                        {
                            hostVariables.Add(newKey, newValue);
                        }
                    }

                    var replace = VariableLookup(section.ParseReplace, hostVariables);
                    return postParse(replace);
                }
            }
            else
            {
                return postParse(input);
            }

            return default(T);
        }
        
        public static string VariableLookup(string input, Dictionary<string, string> hostVariables)
        {
            if (hostVariables != null && !string.IsNullOrWhiteSpace(input))
            {
                var lookup = Regex.Replace(
                    input,
                    @"\$\((.[^\)]*)\)",
                    new MatchEvaluator((Match e) =>
                    {
                        if (e.Success)
                        {
                            var key = e.Groups[1].Value;
                            if (hostVariables.ContainsKey(key))
                            {
                                return hostVariables[key];
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
