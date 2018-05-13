using DMR11.Core.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11
{
    public interface IBookmark
    {
        /// <summary>
        /// Gets or sets the series name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the display name of the host website.
        /// </summary>
        string Site { get; set; }

        /// <summary>
        /// Gets or sets the URI of the website hosting the series.
        /// </summary>
        Uri SeriesUri { get; set; }

        /// <summary>
        /// Gets or sets the series save destination on the disk.
        /// </summary>
        string SaveDestination { get; set; }

        /// <summary>
        /// Gets or sets whether the series has been completely downloaded.
        /// </summary>
        /// <remarks>Unused.</remarks>
        bool Completed { get; set; }

        /// <summary>
        /// Gets or sets the date when the series was bookmarked.
        /// </summary>
        long DateAdded { get; set; }

        /// <summary>
        /// Gets or sets the date when the series has last been updated.
        /// </summary>
        long DateUpdated { get; set; }
    }
}
