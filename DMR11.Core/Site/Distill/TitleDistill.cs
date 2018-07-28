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

        public TitleDistill(Uri address)
            : this(address, null)
        {
        }

        public TitleDistill(Uri address, ILogger log)
            : this(address, LoadConfigFile(address), log)
        {
        }

        public TitleDistill(Uri address, IWebsiteHost hostData, ILogger log)
            : base(address, log)
        {
            if (address == null)
            {
                throw new NullReferenceException("Address cannot be null");
            }

            HostData = hostData;

            /// Sets the host URI of the series.
            HostVariables.Add("host", address.Host);

            /// Sets the host URI of the series including its scheme (ie. https://).
            HostVariables.Add("host_and_scheme", string.Concat(address.Scheme, Uri.SchemeDelimiter, address.Host));

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
            IWebsiteHost data = null;
            return TryGetDistilledHost(uri, out data);
        }

        public static bool TryGetDistilledHost(Uri uri, out IWebsiteHost result)
        {
            var isDistilledHost = false;

            result = LoadConfigFile(uri);

            if (result != null)
            {
                /// Todo: Refactor, check if the chapter URI is not null and if chapter URI are the only supported URIs.
                /// If so, then don't return true when matching the host name. e.g. Chapter URI > Host URI

                var regexOption = RegexOptions.Compiled | RegexOptions.IgnoreCase;

                // Assumes regex.
                if (Regex.IsMatch(uri.Host, result.Host.HostUriPattern, regexOption)
                    || (result.Host.ChapterUriPattern != null && Regex.IsMatch(uri.ToString(), result.Host.ChapterUriPattern, regexOption)))
                {
                    isDistilledHost = true;
                }
            }

            return isDistilledHost;
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

            var seriesTitle = (title.Count > 0 && !string.IsNullOrWhiteSpace(title[0])) ? title[0] : "Untitled";

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
                            (chapterUri) => ChapterParseActionUriSupplied(new DMR11.Core.Net.ValidatedUri(chapterUri), element, parseDetails, html)
                        );
                    },
                    Log,
                    HostVariables
                );

                return Parsing.ParseChapters(html, details);
            }

        }

        public IChapter ChapterParseActionUriSupplied(Uri chapterUri, HtmlNode element, IParseDetails<IChapter> parseDetails, string html = null)
        {
            var chapterTitle = ParseChapterTitle(element, html);

            var chapter = new ChapterDistill(chapterTitle, chapterUri, this.HostData, Log);

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

        string ParseChapterTitle(HtmlNode element, string html)
        {
            if (string.IsNullOrWhiteSpace(HostData.Chapters.Title) || string.IsNullOrWhiteSpace(HostData.Chapters.TitleValue))
            {
                return GetFirstNonEmptyNodeText(element);
            }
            else
            {
                string chapterTitle = null;

                /// Check if the path has the meta variable '__literal' for literal values. If not,
                /// assume it points to an XPATH.
                if (Parsing.IsMetaVariableLiteral(HostData.Chapters.Title))
                {
                    /// Since the 'chapter' variable will become available after this method executes with the
                    /// creation of a new 'Chapter' instance, we'll have to register the variable now in order
                    /// to gain access to it. It should be noted that setting the chapter's name in this manner
                    /// does not take the default chapter format's place. It effectively overwrites the
                    /// chapter's name.

                    Parsing.RegisterChapterVariable(GetFirstNonEmptyNodeText(element), HostVariables);
                    chapterTitle = Parsing.EvaluateVariable(HostData.Chapters.TitleValue, HostVariables);
                }
                else
                {
                    var details = new ParseDetails<string>(
                        HostData.Chapters.Title,
                        HostData.Chapters.TitleValue,
                        (_, parseDetails) =>
                        {
                            return SectionGenericParseAction(
                                _,
                                parseDetails,
                                HostData.Chapters.TitleParseRegex,
                                HostData.Chapters.TitleParseReplace,
                                (title) => title
                            );
                        },
                        Log
                    );

                    var results = Parsing.ParseContentFromNode(element, details);
                    chapterTitle = results.Count > 0 ? results[0] : null;
                }

                return chapterTitle;
            }
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
            return Parsing.SectionGenericParseAction(element, details, section.ParseRegex, section.ParseReplace, postParse, HostVariables);
        }

        public T SectionGenericParseAction<T>(HtmlNode element, IParseDetails<T> details, string parseRegex, string parseReplace, Func<string, T> postParse)
        {

            return Parsing.SectionGenericParseAction<T>(element, details, parseRegex, parseReplace, postParse, HostVariables);
        }

    }
}
