using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MangaRipper.Core.Helper
{
    public class ValidatedUri : Uri
    {
        public ValidatedUri(string uri)
            : base(CheckAndInsertMissingScheme(uri))
        {
        }

        public static Uri CreateUri(string uri)
        {
            uri = CheckAndInsertMissingScheme(uri);
            return new Uri(uri);
        }

        /// <summary>
        /// Checks if the URI is missing the HTTP or HTTPS scheme.
        /// </summary>
        /// <param name="uri">The URI to check.</param>
        /// <param name="preferredScheme">The scheme to insert if it is missing one.</param>
        /// <returns></returns>
        public static string CheckAndInsertMissingScheme(string uri, string preferredScheme = "http")
        {
            var missingSchemePattern = "^(?!http[s]:)(?=//)";

            if (System.Text.RegularExpressions.Regex.IsMatch(uri, missingSchemePattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                // Insert the missing colon if the preferred scheme doesn't end with one.
                if (!preferredScheme.EndsWith(":"))
                {
                    preferredScheme = string.Concat(preferredScheme, ":");
                }

                // Return the uri with the preferred scheme prefixed.
                return uri.Insert(0, preferredScheme);
            }
            else
            {
                // Return the unchanged value.
                return uri;
            }

        }
    }
}
