using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Core.Net
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
}
