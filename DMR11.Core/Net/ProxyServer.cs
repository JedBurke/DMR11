using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Core.Net
{
    public class ProxyServer
    {
        private static readonly Lazy<ProxyServer> lazyInstance = new Lazy<ProxyServer>(() => new ProxyServer());

        public static ProxyServer Instance
        {
            get
            {
                return lazyInstance.Value;
            }
        }

        private ProxyServer()
        {

        }

        public IWebProxy Proxy
        {
            get;
            set;
        }

        // Todo: Bind properties.

        public void SetProxyServer(string host, int port, string user = null, string pass = null)
        {
            var proxy = new WebProxy(host, port);
            proxy.BypassProxyOnLocal = true;

            if (!string.IsNullOrWhiteSpace(user))
            {
                proxy.Credentials = new NetworkCredential(user, pass);
            }

            Proxy = proxy;
        }

        public bool UseProxyServer
        {
            get;
            set;
        }

        public string GetProxyIpAddress()
        {
            var ipAddress = string.Empty;
            var ipVerifiereUri = new ValidatedUri("https://www.ipaddress.com/");

            var response = Service.Downloader.Instance.DownloadString(ipVerifiereUri);
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(response);

            var ipNode = doc.DocumentNode.SelectSingleNode("//p[@id='ipv4']/a[contains(@class, 'ip')]");

            if (ipNode == null || string.IsNullOrWhiteSpace(ipNode.InnerText))
            {
                ipAddress = "0.0.0.0";
            }
            else
            {
                ipAddress = ipNode.InnerText;
            }

            return ipAddress;
        }

    }
}
