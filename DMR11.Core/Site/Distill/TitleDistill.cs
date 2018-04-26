﻿using IniParser.Parser;
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

        //IWebsiteHost HostData = null;

        public TitleDistill(UriValidated address)
            : base(address)
        {
            HostData = LoadConfigFile(address);

            HostVariables.Add("host", address.Host);
            HostVariables.Add("address", address.ToString());
            HostVariables.Add("address_trimmed", address.ToString().Substring(0, address.ToString().LastIndexOf('/')));
            HostVariables.Add("series_name", this.SeriesTitle);
   
        }

        /// <summary>
        /// Returns whether the application has support for distilled host plugins.
        /// </summary>
        /// <param name="uri">The service's URI to check.</param>
        /// <returns></returns>
        public static bool IsDistilled(UriValidated uri)
        {
            IWebsiteHost data = LoadConfigFile(uri);
            
            if (data != null)
            {
                // Assume regex.
                if (Regex.IsMatch(uri.Host, data.Host.HostUriPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase))
                {
                    return true;
                }
            }
            
            return false;
        }

        private static IWebsiteHost LoadConfigFile(UriValidated uri)
        {
            var configPath = LookupConfigPath(uri);

            var serializer = new IniWebsiteHostSerializer();
            return serializer.SeralizeFromPath(configPath);  
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


        static ILogger logger = LogManager.GetCurrentClassLogger();

        protected override string ParseSeriesTitle(string html)
        {
            logger.Debug("Entering ParseSeriesTitle");

            string path = HostData.Title.Path;
            string pathvalue = HostData.Title.Value;

            var details = new ParseDetails<string>(path, pathvalue, TitleParseAction, logger);
            var title = Parsing.ParseContent<string>(html, details);

            return (title != null && title.Count > 0) ? title[0] : "Untitled";
        }

        protected override List<IChapter> ParseChapterObjects(string html)
        {
            logger.Debug("Entering ParseChapterObjects");
            //logger.Trace("Html parameter: {0}", html);

            string path = HostData.Chapters.Path;
            string pathValue = HostData.Chapters.Value;
            
            var details = new ChapterParseDetails(path, pathValue, ChapterParseAction, logger,  HostVariables);
            return Parsing.ParseChapters(html, details);
        }

        public IChapter ChapterParseAction(HtmlNode element, IParseDetails<IChapter> parseDetails)
        {
            var uri = Parsing.CreateUriFromElementAttributeValue(element, parseDetails, Address);
            var chapter = new ChapterDistill(element.InnerText, uri, this.HostData);
            
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
    }
}
