using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Core.WebsiteHost
{
    public class WebsiteHost : IWebsiteHost
    {
        public const string CONFIG_FILE_NAME = "config.ini";

        private IMetaSection _meta;
        private IMainSection _host;
        private IHostSection 
            _title,
            _chapters,
            _pages,
            _page;

        public WebsiteHost()
        {
            _meta = new MetaSection();
            _host = new MainSection();
            _title = new HostSection();
            _chapters = new HostSection();
            _pages = new HostSection();
            _page = new HostSection();
        }

        public IMetaSection Meta
        {
            get { return _meta; }
        }

        public IMainSection Host
        {
            get { return _host; }
        }

        public IHostSection Title
        {
            get { return _title; }
        }

        public IHostSection Chapters
        {
            get { return _chapters; }
        }

        public IHostSection Pages
        {
            get { return _pages; }
        }

        public IHostSection Page
        {
            get { return _page; }
        }
    }
}
