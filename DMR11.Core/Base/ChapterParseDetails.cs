using HtmlAgilityPack;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Core.Helper
{
    public class ChapterParseDetails : IParseDetails<IChapter>
    {
        public ChapterParseDetails(string xpath, string attributeName, Func<HtmlNode, IParseDetails<IChapter>, IChapter> parseAction, ILogger logger, IDictionary<string, string> hostVariables)
        {
            XPath = xpath;
            AttributeName = attributeName;
            ParseAction = parseAction;
            this.Logger = logger;
            this.HostVariables = hostVariables;
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

        public Func<HtmlNode, IParseDetails<IChapter>, IChapter> ParseAction
        {
            get;
            set;
        }

        public ILogger Logger
        {
            get;
            set;
        }

        public IDictionary<string, string> HostVariables
        {
            get;
            set;
        }
    }
}
