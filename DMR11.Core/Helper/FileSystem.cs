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

        public static string GetSafePath(string path)
        {
            return GetSafePath(path, string.Empty);
        }

        public static string[] GetReservedFileNames()
        {
            var invalidFileNames = new[] {
                "CON",
                "PRN",
                "AUX",
                "NUL",
                "COM1",
                "COM2",
                "COM3",
                "COM4",
                "COM5",
                "COM6",
                "COM7",
                "COM8",
                "COM9",
                "LPT1",
                "LPT2",
                "LPT3",
                "LPT4",
                "LPT5",
                "LPT6",
                "LPT7",
                "LPT8",
                "LPT9"
            };

            return invalidFileNames;
        }

        public static bool IsPathValid(string path)
        {
            var cotainsReserved = true;
            var root = Path.GetPathRoot(path);
            var rootRemoved = path.Remove(0, root.Length);

            foreach (var part in splitPath(rootRemoved))
            {
                // Check if the file names start with reserved file names.
                cotainsReserved = GetReservedFileNames().Any(
                    invalidFileName => part.StartsWith(string.Concat(invalidFileName + "."), StringComparison.CurrentCultureIgnoreCase)
                );

                if (cotainsReserved)
                {
                    return false;
                }
                else
                {
                    foreach (var partChar in part)
                    {
                        if (Path.GetInvalidFileNameChars().Contains(partChar))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private static string[] splitPath(string path)
        {
            return path.Split(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });
        }

        public static string GetSafePath(string path, string replacement)
        {
            // See for more information
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365247%28v=vs.85%29.aspx#naming_conventions

            // Remove root.
            var root = Path.GetPathRoot(path);

            var rootRemoved = path.Remove(0, root.Length);

            var invalidChars = Path.GetInvalidFileNameChars().ToList();
            invalidChars.Remove(Path.DirectorySeparatorChar);
            invalidChars.Remove(Path.AltDirectorySeparatorChar);
            
            var fileName = Path.GetFileName(rootRemoved);
            rootRemoved = rootRemoved.Remove(rootRemoved.Length - fileName.Length, fileName.Length);
            fileName = GetSafeFileName(fileName);

            return Path.Combine(root, string.Join("", Path.Combine(rootRemoved, fileName).Split(invalidChars.ToArray()))).Trim();
        }
    }
}
