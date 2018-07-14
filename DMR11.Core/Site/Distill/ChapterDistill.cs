using DMR11.Core.WebsiteHost;
using DMR11.Core.Net;
using HtmlAgilityPack;
using DMR11.Core.Helper;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DMR11.Core
{
    public class ChapterDistill : ChapterBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IWebsiteHost HostData;

        public ChapterDistill(string name, Uri address, IWebsiteHost hostData)
            : base(name, address)
        {

            if (hostData == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                HostData = hostData;

                //Referrer = HostData["host"]["referer"];

                /// Sets the chapter's name according to the host.
                HostVariables.Add("chapter", Name);

                /// Sets the chapter's address.
                HostVariables.Add("address", address.ToString());

                /// Sets a trimmed version of the chapter's address.
                HostVariables.Add("address_trimmed", address.ToString().Substring(0, address.ToString().LastIndexOf('/')));
                                
                // Short-circuit the page listing if all of the 'pages' (chapter images) are in a single HTML page.
                SinglePage = HostData.Host.SinglePage;
                
            }
        }

        protected override List<Uri> ParseImageAddresses(string html)
        {
            var details = new ParseDetails<Uri>
            (
                HostData.Page.Path,
                HostData.Page.Value,
                (element, parseDetails) => GenericParseAction(element, parseDetails, HostData.Page, (uri) => new ValidatedUri(uri)),
                logger
            );

            return Parsing.ParseAddresses(html, details);
        }

        protected override List<Uri> ParsePageAddresses(string html)
        {
            if (SinglePage)
            {
                return new List<Uri>();
            }
            else
            {
                var details = new ParseDetails<Uri>(
                    HostData.Pages.Path,
                    HostData.Pages.Value,
                    (element, parseDetails) => GenericParseAction(element, parseDetails, HostData.Pages, (uri) => new ValidatedUri(uri)),
                    logger
                );
                
                return Parsing.ParseAddresses(html, details);
            }
        }

        public T GenericParseAction<T>(HtmlNode element, IParseDetails<T> details, IHostSection section, Func<string, T> postParse)
        {
            return Parsing.GenericParseAction<T>(element, details, section, postParse, HostVariables);
        }
        
    }
}
