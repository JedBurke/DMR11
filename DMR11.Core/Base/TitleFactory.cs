using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMR11.Core
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
            
            if (TitleDistill.IsDistilled(uri))
            {
                title = new TitleDistill(uri);
            }
            else
            {
                string message = String.Format("'{0}' is currently unsupported.", uri.Host);
                throw new Exception(message);
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
