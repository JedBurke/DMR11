using IniParser.Parser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using NLog;
using HtmlAgilityPack;
using DMR11.Core.Helper;
using DMR11.Core.WebsiteHost;

namespace DMR11.Core
{
    public class TitleDistill : TitleBase
    {
        const string HOST_LOOKUP_PATH = "hosts";
        
        public TitleDistill(Uri address) : this(address, null)
        {
        }

        public TitleDistill(Uri address, ILogger log) 
            : base(address, log)
        {
            if (address == null)
            {
                throw new NullReferenceException("Address cannot be null");
            }
            
            HostData = LoadConfigFile(address);

            /// Sets the host URI of the series.
            HostVariables.Add("host", address.Host);

            /// Sets the series' main address.
            HostVariables.Add("series_address", address.ToString());

            /// Set the the trimmed address for the main series page.
            HostVariables.Add("series_address_trimmed", address.ToString().Substring(0, address.ToString().LastIndexOf('/')));

            /// Sets the series name.
            HostVariables.Add("series_name", this.SeriesTitle);

            if (IsAddressChapterUri())
            {
                IsChapter = true;
            }
        }

        private bool IsAddressChapterUri()
        {
            return IsAddressChapterUri(Address.ToString());
        }

        private bool IsAddressChapterUri(string address)
        {
            var isChapter = false;

            if (!string.IsNullOrWhiteSpace(HostData.Host.ChapterUriPattern))
            {
                isChapter = Regex.IsMatch(address, HostData.Host.ChapterUriPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            }

            return isChapter;
        }

        /// <summary>
        /// Returns whether the application has support for distilled host plugins.
        /// </summary>
        /// <param name="uri">The service's URI to check.</param>
        /// <returns></returns>
        public static bool IsDistilled(Uri uri)
        {
            IWebsiteHost data = LoadConfigFile(uri);
            
            if (data != null)
            {
                /// Todo: Refactor, check if the chapter URI is not null and if chapter URI are the only supported URIs.
                /// If so, then don't return true when matching the host name. e.g. Chapter URI > Host URI
                
                // Assume regex.
                if (Regex.IsMatch(uri.Host, data.Host.HostUriPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase))
                {
                    return true;
                }
                else if (data.Host.ChapterUriPattern != null && Regex.IsMatch(uri.ToString(), data.Host.ChapterUriPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase))
                {
                    return true;
                }
            }
            
            return false;
        }

        private static IWebsiteHost LoadConfigFile(Uri uri)
        {
            var configPath = LookupConfigPath(uri);

            var serializer = new IniWebsiteHostSerializer();
            var host = serializer.SeralizeFromPath(configPath);

            return host;
        }

        private static string LookupConfigPath(Uri uri)
        {
            return LookupConfigPath(uri.Host);
        }

        private static string LookupConfigPath(string host)
        {
            var paths = Directory.EnumerateDirectories(HOST_LOOKUP_PATH);
            foreach (var path in paths)
            {
                if (host.StartsWith("www.") || host.StartsWith("raw."))
                {
                    host = host.Substring(4);
                }

                string hostName = host.Substring(0, host.LastIndexOf('.'));

                string directoryName = Path.GetFileName(path);

                if (string.Compare(directoryName, hostName, true) == 0)
                {
                    return Path.Combine(path, WebsiteHost.WebsiteHost.CONFIG_FILE_NAME);
                }
            }

            return string.Empty;
        }

        private static IWebsiteHost SerializeIniConfigFile(string path)
        {
            var serializer = new IniWebsiteHostSerializer();
            return serializer.SeralizeFromPath(path);            
        }


        protected override string ParseSeriesTitle(string html)
        {
            Log.Debug("Entering ParseSeriesTitle");

            string path = HostData.Title.Path;
            string pathvalue = HostData.Title.Value;

            var details = new ParseDetails<string>
            (
                path,
                pathvalue,
                (element, parseDetails) =>
                {
                    return Parsing.GenericParseAction<string>(
                        element,
                        parseDetails,
                        HostData.Title,
                        (filteredTitle) => filteredTitle,
                        HostVariables
                    );
                },
                Log
            );

            var title = Parsing.ParseContent<string>(html, details);

            var seriesTitle = (title != null && !string.IsNullOrWhiteSpace(title[0])) ? title[0] : "Untitled";

            // Todo: Define constant.
            HostVariables["series_name"] = seriesTitle;
            
            return seriesTitle;
        }

        protected override List<IChapter> ParseChapterObjects(string html)
        {
            Log.Debug("Entering ParseChapterObjects");

            if (IsChapter)
            {
                Log.Debug("The supplied URI is for a chapter.");

                var chapter = new List<IChapter>();
                chapter.Add(new ChapterDistill(SeriesTitle, Address, HostData, Log));
                
                return chapter;
            }
            else
            {
                string path = HostData.Chapters.Path;
                string pathValue = HostData.Chapters.Value;

                var details = new ChapterParseDetails(
                    path,
                    pathValue,
                    (element, parseDetails) =>
                    {
                        return GenericParseAction<IChapter>
                        (
                            element,
                            parseDetails,
                            HostData.Chapters,
                            (chapterUri) => ChapterParseActionUriSupplied(new DMR11.Core.Net.ValidatedUri(chapterUri), element, parseDetails)
                        );
                    },
                    Log,
                    HostVariables
                );

                return Parsing.ParseChapters(html, details);
            }

        }

        public IChapter ChapterParseActionUriSupplied(Uri chapterUri, HtmlNode element, IParseDetails<IChapter> parseDetails)
        {
            // Todo: Allow the user to set the chapter title.
            var chapterText = GetFirstNonEmptyNodeText(element);
                        
            var chapter = new ChapterDistill(chapterText, chapterUri, this.HostData, Log);

            ((Core.Helper.ChapterParseDetails)parseDetails).HostVariables.ToList().ForEach((pair) =>
            {
                if (chapter.HostVariables.ContainsKey(pair.Key))
                {
                    chapter.HostVariables[pair.Key] = pair.Value;
                }
                else
                {
                    chapter.HostVariables.Add(pair.Key, pair.Value);
                }
            });

            return chapter ?? null;
        }

        string GetFirstNonEmptyNodeText(HtmlNode element)
        {
            var text = string.Empty;

            /// Check if the element has children. If so, ennumerate them until
            /// one is found that has text.
            if (element.HasChildNodes)
            {
                foreach (var child in element.ChildNodes)
                {
                    if (child != null && child.InnerText != null && !string.IsNullOrWhiteSpace(child.InnerText.Trim()))
                    {
                        text = child.InnerText;
                        break;
                    }
                }
            }

            /// Use the element's text if the element has no children or the children text cannot be set.
            if (string.IsNullOrWhiteSpace(text))
            {
                text = element.InnerText;
            }

            if (!string.IsNullOrWhiteSpace(text))
            {
                text = text.Trim();
            }

            return text;
        }

        public IChapter ChapterParseAction(HtmlNode element, IParseDetails<IChapter> parseDetails)
        {
            var uri = Parsing.CreateUriFromElementAttributeValue(element, parseDetails, Address);
            var chapter = new ChapterDistill(element.InnerText, uri, this.HostData, Log);
            
            ((Core.Helper.ChapterParseDetails)parseDetails).HostVariables.ToList().ForEach((pair) =>
            {
                if (chapter.HostVariables.ContainsKey(pair.Key))
                {
                    chapter.HostVariables[pair.Key] = pair.Value;
                }
                else
                {
                    chapter.HostVariables.Add(pair.Key, pair.Value);
                }
            });

            return chapter ?? null;
        }

        public string TitleParseAction(HtmlNode element, IParseDetails<string> parseDetails)
        {
            string title = string.Empty;

            // Todo: Replace with correct function call.
            if (string.Compare(parseDetails.AttributeName, "$(__inner_text)", true) == 0)
            {
                title = element.InnerText;
            }
            else
            {
                title = element.GetAttributeValue(parseDetails.AttributeName, null);
            }

            return title;
        }


        public T GenericParseAction<T>(HtmlNode element, IParseDetails<T> details, IHostSection section, Func<string, T> postParse)
        {
            return Parsing.GenericParseAction<T>(element, details, section, postParse, HostVariables);
        }
        
    }
}
