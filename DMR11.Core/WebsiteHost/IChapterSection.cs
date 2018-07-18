using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Core.WebsiteHost
{
    public interface IChapterSection : IHostSection
    {
        string Title
        {
            get;
            set;
        }

        string TitleValue
        {
            get;
            set;
        }

        string TitleParseRegex { get; set; }

        string TitleParseReplace { get; set; }

        /// <summary>
        /// Gets or sets the path to the element which goes to the next page.
        /// </summary>
        string Pager
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the attribute used to access the pager element's value.
        /// </summary>
        string PagerValue
        {
            get;
            set;
        }
    }
}
