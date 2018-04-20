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

namespace DMR11.Core
{
    public class TitleDistill : TitleBase
    {
        const string HOST_LOOKUP_PATH = "hosts";
        const string CONFIG_FILE_NAME = "config.ini";

        IniData HostData = null;

        public TitleDistill(UriValidated address)
            : base(address)
        {
            HostData = LoadConfigFile(address);
            
        }

        /// <summary>
        /// Returns whether the application has support for distilled host plugins.
        /// </summary>
        /// <param name="uri">The service's URI to check.</param>
        /// <returns></returns>
        public static bool IsDistilled(UriValidated uri)
        {
            IniData data = LoadConfigFile(uri);
            
            if (data != null)
            {
                string hostType = data["host"]["type"];
                string hostPattern = data["host"]["uri"];

                // Assume regex.
                if (Regex.IsMatch(uri.Host, hostPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase))
                {
                    return true;
                }
            }
            
            return false;
        }

        private static IniData LoadConfigFile(UriValidated uri)
        {
            var configPath = LookupConfigPath(uri);
            return GetConfigFile(configPath);
        }

        private static string LookupConfigPath(UriValidated uri)
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
                    return Path.Combine(path, CONFIG_FILE_NAME);
                }
            }

            return string.Empty;
        }

        private static IniData GetConfigFile(string path)
        {
            if (File.Exists(path))
            {
                var contents = File.ReadAllText(path);
                var parser = new IniDataParser();

                return parser.Parse(contents);

            }

            return null;
        }

        static ILogger logger = LogManager.GetCurrentClassLogger();

        protected override string ParseSeriesTitle(string html)
        {
            logger.Debug("Entering ParseSeriesTitle");

            string path = HostData["title"]["path"];
            string pathvalue = HostData["title"]["value"];

            var details = new ParseDetails<string>(path, pathvalue, TitleParseAction, logger);
            var title = Parsing.ParseContent<string>(html, details);

            return (title != null && title.Count > 0) ? title[0] : "Untitled";
        }

        protected override List<IChapter> ParseChapterObjects(string html)
        {
            logger.Debug("Entering ParseChapterObjects");
            //logger.Trace("Html parameter: {0}", html);

            string path = HostData["chapters"]["path"];
            string pathValue = HostData["chapters"]["value"];

            var details = new ChapterParseDetails(path, pathValue, ChapterParseAction, logger);
            return Parsing.ParseChapters(html, details);
        }

        public IChapter ChapterParseAction(HtmlNode element, IParseDetails<IChapter> parseDetails)
        {
            var uri = Parsing.CreateUriFromElementAttributeValue(element, parseDetails, Address);
            var chapter = new ChapterDistill(element.InnerText, uri, this.HostData);

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
    }
}
