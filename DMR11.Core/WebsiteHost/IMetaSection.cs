using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Core.WebsiteHost
{
    /// <summary>
    /// Defines the section for the website host plugin which stores metadata.
    /// </summary>
    public interface IMetaSection
    {
        /// <summary>
        /// Gets the host plugin type.
        /// </summary>
        HostType HostType { get; set; }

        /// <summary>
        /// Gets the path to the auxiliary script file.
        /// </summary>
        string ScriptPath { get; set; }

        /// <summary>
        /// Gets or sets the maximum version which this plugin has been tested.
        /// </summary>
        Version MaximumVersion { get; set; }

        /// <summary>
        /// Gets or sets the minimum version which this plugin has been tested.
        /// </summary>
        Version MinimumVersion { get; set; }

    }
}
