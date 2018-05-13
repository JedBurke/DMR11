using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DMR11.Core.Net
{
    /// <summary>
    /// Represents a decorator class for the Uri class which automatically injects the inferred scheme.
    /// </summary>
    public class ValidatedUri : UriDecorator
    {
        /// <summary>
        /// Creates a new instance of ValidatedUri by wrapping an existing Uri instance.
        /// </summary>
        /// <param name="decorated">The Uri instance to be decorated.</param>
        public ValidatedUri(Uri decorated)
            : base(decorated)
        {
        }

        /// <summary>
        /// Creates a new instance of ValidatedUri from a string representing the Uri.
        /// </summary>
        /// <param name="uri">The string to be instantized as a ValidateUri.</param>
        public ValidatedUri(string uri)
            : base(new Uri(CheckAndInsertMissingScheme(uri)))
        {            
            
        }
        
        /// <summary>
        /// Represents a regular expression pattern used to check if the URI is missing its scheme.
        /// </summary>
        protected static readonly string MissingSchemeTestPattern = "^(?!http[s]:)(?=//)";

        /// <summary>
        /// Checks if the URI is missing the HTTP or HTTPS scheme.
        /// </summary>
        /// <param name="uri">The URI to check.</param>
        /// <param name="preferredScheme">The scheme to insert if it is missing one.</param>
        /// <returns>The URI with the missing scheme.</returns>
        public static string CheckAndInsertMissingScheme(string uri, UriScheme preferredScheme = UriScheme.Http)
        {
            if (Regex.IsMatch(uri, MissingSchemeTestPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled))
            {
                var scheme = GetUriSchemeDisplay(preferredScheme);

                // Return the uri with the preferred scheme prefixed.
                return string.Concat(scheme, uri);
            }
            else
            {
                // Return the unchanged value.
                return uri;
            }

        }

        /// <summary>
        /// Gets the display name of a URI scheme.
        /// </summary>
        /// <param name="scheme">The URI scheme which to have its display name returned.</param>
        /// <returns>The display name of the scheme.</returns>
        private static string GetUriSchemeDisplay(UriScheme scheme)
        {
            // Todo: Enhance the enum and make use of the read-only fields.
            return string.Concat(scheme.ToString().ToLower(), ":");
        }
    }
}
