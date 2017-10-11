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
        public static List<UriValidated> ParseAddresses(string html, ParseDetails<UriValidated> details)
        {
            return ParseContent<UriValidated>(html, details);
        }

        public static List<IChapter> ParseChapters(string html, ParseDetails<IChapter> details)
        {
            return ParseContent<IChapter>(html, details);
        }

        public static List<T> ParseContent<T>(string html, ParseDetails<T> details)
        {
            var list = new List<T>();
            var doc = new HtmlDocument();

            try
            {
                doc.LoadHtml(html);

                var elements = doc.DocumentNode.SelectNodes(details.xpath);

                foreach (var element in elements)
                {
                    T obj = default(T);

                    if (details.parseAction != null)
                    {
                        obj = details.parseAction(element, details);

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

        public static UriValidated CreateUriFromElementAttributeValue<T>(HtmlNode element, ParseDetails<T> details) 
        {
            return new UriValidated(element.GetAttributeValue(details.attributeName, null));
        }

    }

    public interface IParseDetails
    {
        string documentHtml { get; set; }
    }

    public struct ParseDetails<T>
    {
        public string xpath;
        public string attributeName;
        public Func<HtmlNode, ParseDetails<T>, T> parseAction;
    }

    public interface IOP<T>
    {

    }
    
    public struct ChapterParseDetails : IOP<IChapter>
    {
        public ChapterParseDetails()
    }
}
