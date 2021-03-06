﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMR11.Core
{
    public static class TitleFactory
    {
        /// <summary>
        /// Create a Title object base on the URI.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static ITitle CreateTitle(Uri uri)
        {
            ITitle title = null;
            WebsiteHost.IWebsiteHost hostData;
            
            if (TitleDistill.TryGetDistilledHost(uri, out hostData))
            {
                title = new TitleDistill(uri, hostData, null);
            }
            else
            {
                string message = String.Format("'{0}' is currently unsupported.", uri.Host);
                throw new Exception(message);
            }

            return title;
        }

        /// <summary>
        /// Gets a list of supported manga sites.
        /// </summary>
        /// <returns></returns>
        public static List<string[]> GetSupportedSites()
        {
            /// Todo: Iterate through the host directories and display the name, lang code, and URI for
            /// the hosts contained within.

            throw new NotImplementedException();
        }
    }
}
