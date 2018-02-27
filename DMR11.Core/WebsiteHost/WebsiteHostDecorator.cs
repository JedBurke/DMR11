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

        public HostSection Title
        {
            get { return decoratedHost.Title; }
        }

        public HostSection Chapters
        {
            get { return decoratedHost.Chapters; }
        }

        public HostSection Pages
        {
            get { return decoratedHost.Page; }
        }

        public HostSection Page
        {
            get { return decoratedHost.Page; }
        }
    }
}
