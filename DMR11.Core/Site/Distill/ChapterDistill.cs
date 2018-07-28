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
        private IWebsiteHost HostData;

        public ChapterDistill(string name, Uri address, IWebsiteHost hostData, ILogger log)
            : base(name, address, log)
        {
            if (hostData == null)
            {
                throw new ArgumentNullException("Variable 'hostData' cannot be null.");
            }
            else
            {
                HostData = hostData;

                /// Sets the chapter's name according to the host.
                Parsing.RegisterChapterVariable(Name, HostVariables);
                Log.Debug("Chapter 'name' variable: {0}", HostVariables[Parsing.CHAPTER_VARIABLE]);

                /// Sets the chapter's address.
                HostVariables.Add("address", address.ToString());
                Log.Debug("Chapter 'address' value: {0}", HostVariables["address"]);

                /// Sets a trimmed version of the chapter's address.
                HostVariables.Add("address_trimmed", address.ToString().Substring(0, address.ToString().LastIndexOf('/')));
                log.Debug("Chapter 'address_trimmed' value: {0}", HostVariables["address_trimmed"]);
                                
                // Short-circuit the page listing if all of the 'pages' (chapter images) are in a single HTML page.
                SinglePage = HostData.Host.SinglePage;
                log.Debug("Is single page: {0}", SinglePage);

            }
        }

        protected override List<Uri> ParseImageAddresses(string html)
        {
            var details = new ParseDetails<Uri>
            (
                HostData.Page.Path,
                HostData.Page.Value,
                (element, parseDetails) => GenericParseAction(element, parseDetails, HostData.Page, (uri) => new ValidatedUri(uri)),
                Log
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
                    Log
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
