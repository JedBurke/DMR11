using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DMR11.Core
{
    public static class Extension
    {
        /// <summary>
        /// Remove characters that cannot using to name folder, file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RemoveFileNameInvalidChar(this String input)
        {
            // Todo: Optimize;
            return input.Replace("\\", "").Replace("/", "").Replace(":", "")
                        .Replace("*", "").Replace("?", "").Replace("\"", "")
                        .Replace("<", "").Replace(">", "").Replace("|", "");
        }
    }
}
