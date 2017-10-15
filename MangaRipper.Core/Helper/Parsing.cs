using NLog;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaRipper.Core.Helper
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

            try
            {
                doc.LoadHtml(html);

                var elements = doc.DocumentNode.SelectNodes(details.XPath);

                foreach (var element in elements)
                {
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
            finally
            {
                doc = null;
            }

            return list.Distinct().ToList();
        }

        public static UriValidated CreateUriFromElementAttributeValue<T>(HtmlNode element, IParseDetails<T> details) 
        {
            return new UriValidated(element.GetAttributeValue(details.AttributeName, null));
        }
        
    }

    public interface ICommand
    {
        ILogger Logger { get; set; }
    }

    public interface IParseDetails<T> : ICommand
    {
        string XPath { get; set; }
        string AttributeName { get; set; }
        Func<HtmlNode, IParseDetails<T>, T> ParseAction { get; set; }
    }

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
