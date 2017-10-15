using HtmlAgilityPack;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaRipper.Core.Helper
{
    public class ChapterParseDetails : IParseDetails<IChapter>
    {
        public ChapterParseDetails(string xpath, string attributeName, Func<HtmlNode, IParseDetails<IChapter>, IChapter> parseAction, ILogger logger)
        {
            XPath = xpath;
            AttributeName = attributeName;
            ParseAction = parseAction;
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
    }
}
