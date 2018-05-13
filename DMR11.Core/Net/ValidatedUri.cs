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
        /// Creates a new ValidatedUri instance of the specified URI.
        /// </summary>
        /// <param name="uri">The string to be instantized as a ValidatedUri.</param>
        public ValidatedUri(string uri)
            : this(uri, UriScheme.Http)
        {            
            
        }

        /// <summary>
        /// Creates a new ValidatedUri instance of the specified URI. 
        /// </summary>
        /// <param name="uri">The string to be instantized as a ValidatedUri.</param>
        /// <param name="defaultScheme">The scheme to be used if the URI's scheme is to be inferred.</param>
        public ValidatedUri(string uri, UriScheme defaultScheme)
            : base(new Uri(CheckAndInsertMissingScheme(uri, defaultScheme)))
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
        /// <returns>Returns the URI with the missing scheme.</returns>
        public static string CheckAndInsertMissingScheme(string uri, UriScheme preferredScheme = UriScheme.Http)
        {
            return IsMissingUriScheme(uri) ? string.Concat(GetUriSchemeDisplay(preferredScheme), uri) : uri;
        }

        /// <summary>
        /// Determines if the requested URI is missing its scheme.
        /// </summary>
        /// <param name="uri">The string-representation of the URI to be checked.</param>
        /// <returns>A boolean result of whether the scheme is to be inferred.</returns>
        public static bool IsMissingUriScheme(string uri)
        {
            return Regex.IsMatch(uri, MissingSchemeTestPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        /// <summary>
        /// Gets the display name of a URI scheme.
        /// </summary>
        /// <param name="scheme">The URI scheme which to have its display name returned.</param>
        /// <returns>Returns the string-represented display name of the scheme.</returns>
        public static string GetUriSchemeDisplay(UriScheme scheme)
        {
            // Todo: Enhance the enum and make use of the read-only fields.
            return string.Concat(scheme.ToString().ToLower(), ":");
        }
    }
}

/// TODO: Fix case where the scheme and delimiter is missing.
/// Example: example.com vs //example.com vs http[s]://example.com
