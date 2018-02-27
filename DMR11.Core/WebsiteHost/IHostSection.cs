using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Core.WebsiteHost
{
    /// <summary>
    /// Defines a generic section used for the website host plugin.
    /// </summary>
    public interface IHostSection
    {
        /// <summary>
        /// Gets or sets the path to the element.
        /// </summary>
        string Path { get; set; }

        /// <summary>
        /// Gets or sets the attribute used to access the element's value.
        /// </summary>
        string Value { get; set; }

        /// <summary>
        /// Gets or sets the regex pattern used to pre-parse the value.
        /// </summary>
        string ParseRegex { get; set; }

        /// <summary>
        /// Gets or sets the value used to replace the matched value.
        /// </summary>
        string ParseReplace { get; set; }
    }
}
