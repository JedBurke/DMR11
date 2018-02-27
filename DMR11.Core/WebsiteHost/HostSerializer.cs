using IniParser.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Core.WebsiteHost
{
    public class HostSerializer : IDisposable
    {
        IniParser.Model.IniData data;


        public WebsiteHostDecorator Serialize(string path)
        {
            data = LoadIniFile(path);
            
            // Todo: Implement reusable serializer.

            return null;
        }

        private IniParser.Model.IniData LoadIniFile(string path)
        {
            using (var stream = new System.IO.StreamReader(path))
            {
                var parser = new IniParser.StreamIniDataParser();
                return parser.ReadData(stream);
            }
        }

        public void Dispose()
        {
            data = null;
        }
    }
}
