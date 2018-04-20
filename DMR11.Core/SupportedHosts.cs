using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Core
{
    public class SupportedHostsManager
    {
        private bool _watchSearchPath = false;

        private FileSystemWatcher searchPathWatcher;

        public List<SupportedHost> Hosts { get; private set; }
        public string SearchPath { get; private set; }

        public SupportedHostsManager(string searchPath)
        {
            Hosts = new List<SupportedHost>();
            SearchPath = searchPath;

        }

        public bool WatchSearchPath
        {
            get
            {
                return _watchSearchPath;
            }
            set
            {
                _watchSearchPath = value;
                throw new NotImplementedException();
            }
        }

        public void LoadHosts(string searchPath)
        {
            if (Directory.Exists(searchPath))
            {
                var directories = Directory.GetDirectories(searchPath);

                foreach (var directory in directories)
                {
                    var configPath = Path.Combine(directory, "config.ini");

                    if (File.Exists(configPath))
                    {

                    }
                }
            }
        }
    }

    public struct SupportedHost
    {
        public string FriendlyName { get; private set; }
        public string HostPattern { get; private set; }
        public string HostPath { get; private set; }

        public SupportedHost(string friendlyName, string hostPattern, string hostPath) : this()
        {
            if (string.IsNullOrWhiteSpace(friendlyName) || string.IsNullOrWhiteSpace(hostPattern) || string.IsNullOrWhiteSpace(hostPath))
            {
                throw new ArgumentNullException();
            }

            FriendlyName = friendlyName;
            HostPattern = hostPattern;
            HostPath = hostPath;

            if (!System.IO.Directory.Exists(hostPath))
            {
                throw new System.IO.FileNotFoundException();
            }

        }
    }
}
