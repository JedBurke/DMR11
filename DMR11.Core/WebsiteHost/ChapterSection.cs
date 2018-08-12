using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Core.WebsiteHost
{
    public class ChapterSection : IChapterSection
    {
        // Todo: Fix inheritence.

        public string Title
        {
            get;
            set;
        }

        public string TitleValue
        {
            get;
            set;
        }

        public string TitleParseRegex
        {
            get;
            set;
        }

        public string TitleParseReplace
        {
            get;
            set;
        }

        public string Pager
        {
            get;
            set;
        }

        public string PagerValue
        {
            get;
            set;
        }

        public string Path
        {
            get;
            set;
        }

        public string Value
        {
            get;
            set;
        }

        public string ParseRegex
        {
            get;
            set;
        }

        public string ParseReplace
        {
            get;
            set;
        }

        public bool NoChapterDirectory
        {
            get;
            set;
        }

        public ChapterSection()
        {
        }
    }
}
