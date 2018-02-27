using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Core
{
    public class HostTaxonomy
    {
        string _searchDirectory = string.Empty;
        string _fallbackDirectory = string.Empty;

        public string SearchDirectory
        {
            get
            {
                return _searchDirectory;
            }
            set
            {
                _searchDirectory = value;
            }
        }

        public string FallbackDirectory
        {
            get
            {
                return _fallbackDirectory;
            }
            set
            {
                _fallbackDirectory = value;
            }
        }

        public string GetDirectory(Uri taxonomy)
        {
            return HostTaxonomy.GetDirectory(taxonomy, this.SearchDirectory, this.FallbackDirectory);
        }

        public static string GetDirectory(Uri taxonomy, string searchDirectory, string defaultDirectory = "")
        {
            if (taxonomy == null)
                throw new NullReferenceException();

            string search = taxonomy.Host;

            int parts = InstanceOfChar(search, '.');

            var list = new System.Collections.Queue();

            // Take the host as it is.
            list.Enqueue(search);

            
            // Remove the sub-domain.
            //var strippedSubDomain = search.Substring(search.
            

            while (list.Count > 0) {
               var path = Path.Combine(searchDirectory, list.Dequeue().ToString());
               if (Directory.Exists(path))
               {
                   return path;
               }
            }

            return defaultDirectory;
        }

        public static int InstanceOfChar(string input, char searchChar)
        {
            int count = 0;

            if (!string.IsNullOrWhiteSpace(input) && input.IndexOf(searchChar) > -1)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] == searchChar)
                        count++;
                }
            }

            return count;
        }
    }
}
