using DMR11.Core.WebsiteHost;
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

        public ChapterDistill(string name, UriValidated address, IWebsiteHost hostData)
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

                HostVariables.Add("chapter", Name);
                                
                // Short-circuit the page listing if all of the 'pages' (chapter images) are in a single HTML page.
                SinglePage = HostData.Host.SinglePage;
                
            }
        }

        protected override List<UriValidated> ParseImageAddresses(string html)
        {
            var details = new ParseDetails<UriValidated>
            (
                HostData.Page.Path,
                HostData.Page.Value,
                (element, parseDetails) => {
                    return GenericParseAction(
                        element,
                        parseDetails,
                        HostData.Page,
                        (x) => Parsing.CreateUriFromElementAttributeValue(
                            element,
                            parseDetails,
                            new UriValidated(HostVariables["address_trimmed"]
                        )
                    )
                  );
                },
                logger
            );
            
            // (element, parseDetails) => Parsing.CreateUriFromElementAttributeValue(element, parseDetails, new UriValidated(HostVariables["address_trimmed"])),

            return Parsing.ParseAddresses(html, details);
        }

        protected override List<UriValidated> ParsePageAddresses(string html)
        {
            if (SinglePage)
            {
                return new List<UriValidated>();
            }
            else
            {
                var details = new ParseDetails<UriValidated>(
                    HostData.Pages.Path,
                    HostData.Pages.Value,
                    (element, parseDetails) => GenericParseAction(element, parseDetails, HostData.Pages, (uri) => new UriValidated(uri)),
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
