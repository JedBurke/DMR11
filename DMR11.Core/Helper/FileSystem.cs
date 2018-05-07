using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Core.Helper
{
    /// <summary>
    /// Contains helper methods to format and sanitize filesystem objects.
    /// </summary>
    public static class FileSystem
    {
        /// <summary>
        /// Strips invalid path characters from a file name.
        /// </summary>
        /// <param name="fileName">The file name to be sanitized.</param>
        /// <returns>A sanitized file name without invalid path characters.</returns>
        /// <remarks>https://stackoverflow.com/a/12800424</remarks>
        public static string GetSafeFileName(string fileName)
        {
            return GetSafeFileName(fileName, string.Empty);
        }

        /// <summary>
        /// Replaces invalid path characters from a file name.
        /// </summary>
        /// <param name="fileName">The file name to be sanitized.</param>
        /// <param name="replacement">The string used to replace the invalid path characters.</param>
        /// <returns>A sanitized file name without invalid path characters.</returns>
        /// <remarks>https://stackoverflow.com/a/12800424</remarks>
        public static string GetSafeFileName(string fileName, string replacement)
        {
            return string.Join(replacement, fileName.Split(Path.GetInvalidFileNameChars()));
        }
    }
}
