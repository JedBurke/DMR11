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

                details.Logger.Debug("Selected node count: {0}", elements.Count);

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
            var value = element.GetAttributeValue(details.AttributeName, null);
            details.Logger.Debug("Creating validated URI from \"{0}\"", value);

            return new UriValidated(value);
        }
        
    }
    
}
