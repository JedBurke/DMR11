using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MangaRipper.Core
{
    public static class TitleFactory
    {
        /// <summary>
        /// Create Title object base on uri
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static ITitle CreateTitle(UriValidated uri)
        {
            ITitle title = null;
            switch (uri.Host)
            {
                case "kissmanga.com":
                    title = new TitleKissManga(uri);
                    break;
                //case "mangafox.la":
                //    title = new TitleMangaFox(uri);
                //    break;
                case "www.mangahere.co":
                case "www.mangahere.com":
                    title = new TitleMangaHere(uri);
                    break;
                case "www.mangareader.net":
                    title = new TitleMangaReader(uri);
                    break;
                default:
                    {
                        // Check if Host distillation exists for this website.
                        if (TitleDistill.IsDistilled(uri))
                        {
                            title = new TitleDistill(uri);
                        }
                        else
                        {
                            string message = String.Format("This site ({0}) is not supported.", uri.Host);
                            throw new Exception(message);
                        }

                        break;
                    }

            }
            return title;
        }

        /// <summary>
        /// Get list of supported manga sites
        /// </summary>
        /// <returns></returns>
        public static List<string[]> GetSupportedSites()
        {
            var lst = new List<string[]>();
            lst.Add(new string[] { "MangaFox", "http://mangafox.me/", "English" });
            lst.Add(new string[] { "MangaHere", "http://www.mangahere.com/", "English" });
            lst.Add(new string[] { "MangaReader", "http://www.mangareader.net/", "English" });
            lst.Add(new string[] { "MangaShare", "http://read.mangashare.com/", "English" });
            return lst;
        }
    }
}
