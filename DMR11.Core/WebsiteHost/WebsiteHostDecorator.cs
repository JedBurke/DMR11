using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Core.WebsiteHost
{
    public class WebsiteHostDecorator : IWebsiteHost
    {
        public WebsiteHostDecorator(IWebsiteHost decoratedHost)
        {
            this.decoratedHost = decoratedHost;
        }

        IWebsiteHost decoratedHost;


        public IMetaSection Meta
        {
            get { return decoratedHost.Meta; }
        }

        public IMainSection Host
        {
            get { return decoratedHost.Host; }
        }

        public IHostSection Title
        {
            get { return decoratedHost.Title; }
        }

        public IHostSection Chapters
        {
            get { return decoratedHost.Chapters; }
        }

        public IHostSection Pages
        {
            get { return decoratedHost.Page; }
        }

        public IHostSection Page
        {
            get { return decoratedHost.Page; }
        }
    }
}
