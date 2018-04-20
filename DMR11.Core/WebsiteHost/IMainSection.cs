using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Core.WebsiteHost
{
    /// <summary>
    /// Defines the main section for a website host plugin.
    /// </summary>
    public interface IMainSection
    {
        /// <summary>
        /// Gets or sets the display name of the host.
        /// </summary>
        string FriendlyName { get; set; }

        /// <summary>
        /// Gets the pattern used to match the host uri.
        /// </summary>
        string HostUriPattern { get; set; }
        
        /// <summary>
        /// Get or sets the method used to match the host uri.
        /// </summary>
        HostUriType HostUriPatternType { get; set; }
        
        /// <summary>
        /// Gets or sets the the uri where the page is to be redirected.
        /// </summary>
        string RedirectUri { get; set; }

        /// <summary>
        /// Gets or sets whether the host displays all pages on a single webpage.
        /// </summary>
        bool SinglePage { get; set; }

    }
}
