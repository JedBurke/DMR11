using HtmlAgilityPack;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Core.Helper
{
    public class ParseDetails<T> : IParseDetails<T>
    {
        public ParseDetails(string xpath, string attributeName, Func<HtmlNode, IParseDetails<T>, T> parseAction, ILogger logger)
        {
            this.XPath = xpath;
            this.AttributeName = attributeName;
            this.ParseAction = parseAction;
            this.Logger = logger;
        }

        public string XPath
        {
            get;
            set;
        }

        public string AttributeName
        {
            get;
            set;
        }

        public Func<HtmlNode, IParseDetails<T>, T> ParseAction
        {
            get;
            set;
        }

        public ILogger Logger
        {
            get;
            set;
        }
    }

    // Todo: Reduce code complexity.
    // Todo: Use IHostSection instead of specifying the path and values.

    public abstract class ParseDetailsDecorator<T> : IParseDetails<T>
    {
        private readonly IParseDetails<T> _decoratedParseDetails = null;

        public ParseDetailsDecorator(IParseDetails<T> parseDetails)
        {
            this._decoratedParseDetails = parseDetails;
        }
        

        public string XPath
        {
            get
            {
                return _decoratedParseDetails.XPath;
            }
            set
            {
                _decoratedParseDetails.XPath = value;
            }
        }

        public string AttributeName
        {
            get
            {
                return _decoratedParseDetails.AttributeName;
            }
            set
            {
                _decoratedParseDetails.AttributeName = value;
            }
        }

        public Func<HtmlNode, IParseDetails<T>, T> ParseAction
        {
            get
            {
                return _decoratedParseDetails.ParseAction;
            }
            set
            {
                _decoratedParseDetails.ParseAction = value;
            }
        }

        public ILogger Logger
        {
            get
            {
                return _decoratedParseDetails.Logger;
            }
            set
            {
                _decoratedParseDetails.Logger = value;
            }
        }
    }
    
    public class VariableParseDetailsDecorator<T> : ParseDetailsDecorator<T>
    {
        public VariableParseDetailsDecorator(IParseDetails<T> parseDetails) : base(parseDetails)
        {
        }

        public Func<HtmlNode, IParseDetails<T>, T> ParseAction
        {
            get
            {
                return GenericParseAction<T>(base.ParseAction);
            }
            set
            {
                base.ParseAction = value;
            }
        }


        private Func<HtmlNode, IParseDetails<T>, T> GenericParseAction<T>(Func<HtmlNode, IParseDetails<T>, T> passthrough)
        {
            

            return passthrough;
        }


        //private T GenericParseAction<T>(HtmlNode element, IParseDetails<T> details, IHostSection section, Func<string, T> postParse)
        //{
        //    if (!string.IsNullOrWhiteSpace(section.ParseRegex) &&
        //        !string.IsNullOrWhiteSpace(section.ParseReplace))
        //    {
        //        var regex = new Regex(section.ParseRegex);
        //        var match = Match.Empty;

        //        var input = element.GetAttributeValue(details.AttributeName, string.Empty);
        //        if ((match = regex.Match(input)).Success)
        //        {
        //            // Register group values.
        //            foreach (var group in regex.GetGroupNames())
        //            {
        //                var newKey = string.Concat("regex__", group);
        //                var newValue = match.Groups[group].Value;

        //                if (HostVariables.ContainsKey(newKey))
        //                {
        //                    HostVariables[newKey] = newValue;
        //                }
        //                else
        //                {
        //                    HostVariables.Add(newKey, newValue);
        //                }
        //            }

        //            var replace = VariableLookup(HostData.Pages.ParseReplace);
        //            return postParse(replace);
        //        }
        //    }
        //    else
        //    {
        //        var input = element.GetAttributeValue(details.AttributeName, string.Empty);
        //        return postParse(input);
        //    }

        //    return default(T);
        //}

        //private string VariableLookup(string input)
        //{
        //    if (HostVariables != null && !string.IsNullOrWhiteSpace(input))
        //    {
        //        var lookup = Regex.Replace(
        //            input,
        //            @"\$\((.[^\)]*)\)",
        //            new MatchEvaluator((Match e) =>
        //            {
        //                if (e.Success)
        //                {
        //                    var key = e.Groups[1].Value;
        //                    if (HostVariables.ContainsKey(key))
        //                    {
        //                        return HostVariables[key];
        //                    }
        //                }

        //                return input;
        //            })
        //       );

        //        return lookup;
        //    }

        //    return input;
        //}

    }

}
