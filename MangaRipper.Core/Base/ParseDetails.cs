using HtmlAgilityPack;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaRipper.Core.Helper
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
    
}
