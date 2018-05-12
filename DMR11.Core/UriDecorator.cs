using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Core
{
    public interface IDecorator<T>
    {
        T SetDecorated(T decorated);
    }

    public abstract class Decorator<T> : IDecorator<T>
    {
        protected T decorated;
        
        public T SetDecorated(T decorated)
        {
            this.decorated = decorated;
            return this.decorated;
        }
    }

    public abstract class UriDecorator : Decorator<Uri>
    {
        protected Uri decoratedUri;

        public UriDecorator(Uri uri)
        {
            decoratedUri = uri;
            
        }

        public void SetDecoratedUri(Uri uri)
        {
            decoratedUri = uri;
        }

        public new string AbsolutePath
        {
            get
            {
                return decoratedUri.AbsolutePath;
            }
        }

        public override string ToString()
        {
            return decoratedUri.ToString();
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

    public class SwitchUri : UriDecorator
    {
        public SwitchUri(string uri) : this(new Uri(uri))
        {
        }

        public SwitchUri(Uri decorated) : base(decorated)
        {
        }

        public override string ToString()
        {
            return decoratedUri.ToString().ToUpper();
        }
    }


}
