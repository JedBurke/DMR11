using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11
{
    /// <summary>
    /// Describes the status codes used when adding bookmarks.
    /// </summary>
    public enum AddBookmarkStatus : int
    {
        /// <summary>
        /// The bookmark was successfully added.
        /// </summary>
        Success,

        /// <summary>
        /// The addition failed due to the series already having been bookmarked.
        /// </summary>
        Duplicate,

        /// <summary>
        /// There is something wrong with the bookmark's values.
        /// </summary>
        InvalidBookmark,

        /// <summary>
        /// Reserved.
        /// </summary>
        None
    }
}
