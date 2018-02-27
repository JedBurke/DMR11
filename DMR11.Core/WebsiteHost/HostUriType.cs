using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Core.WebsiteHost
{
    /// <summary>
    /// Determines how the host uri is to be matched.
    /// </summary>
    public enum HostUriType : int
    {
        /// <summary>
        /// The host uri is matched literally.
        /// </summary>
        Simple,

        /// <summary>
        /// The host uri is matched with a regular expression.
        /// </summary>
        Regex
    }
}
