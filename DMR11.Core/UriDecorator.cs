using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Core
{
    public abstract class UriDecorator : Uri
    {
        protected Uri decoratedUri;

        public UriDecorator(Uri uri) : base(uri.ToString())
        {
            decoratedUri = uri;
        }

        public void SetDecoratedUri(Uri uri)
        {
            decoratedUri = uri;
        }

    }

    public class ValidateUri : UriDecorator
    {
        public ValidateUri(Uri decorated) : base(decorated)
        {
        }

        public ValidateUri(string uri) : base(new Uri(CheckAndInsertMissingScheme(uri)))
        {            
            
        }

        public enum UriScheme : int
        {
            Http = 0,
            Https,
            Ftp
        }

        /// <summary>
        /// Checks if the URI is missing the HTTP or HTTPS scheme.
        /// </summary>
        /// <param name="uri">The URI to check.</param>
        /// <param name="preferredScheme">The scheme to insert if it is missing one.</param>
        /// <returns></returns>
        public static string CheckAndInsertMissingScheme(string uri, UriScheme preferredScheme = UriScheme.Http)
        {
            var missingSchemePattern = "^(?!http[s]:)(?=//)";

            if (System.Text.RegularExpressions.Regex.IsMatch(uri, missingSchemePattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
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

        private static string GetUriSchemeDisplay(UriScheme scheme)
        {
            // Todo: Enhance the enum and make use of the read-only fields.
            return string.Concat(scheme.ToString().ToLower(), ":");
        }

    }

    public class UpcaseUri : UriDecorator
    {
        public UpcaseUri(string uri) : this(new Uri(uri))
        {
        }

        public UpcaseUri(Uri decorated) : base(decorated)
        {
        }

        public override string ToString()
        {
            return decoratedUri.ToString().ToUpper();
        }
    }


}
