using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Core.Service
{
    public class UserAgent
    {
        private static readonly string[] _userAgents =
        {            
            "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:40.0) Gecko/20100101 Firefox/40.1",
            "Mozilla/5.0 (Windows NT 6.2; Win64; x64; rv:27.0) Gecko/20121011 Firefox/27.0",
            "Mozilla/5.0 (X11; Linux x86_64; rv:28.0) Gecko/20100101 Firefox/28.0",
            "Mozilla/5.0 (Windows NT 6.1; rv:6.0) Gecko/20100101 Firefox/19.0",
            "Mozilla/5.0 (X11; Ubuntu; Linux armv7l; rv:17.0) Gecko/20100101 Firefox/17.0",
            "Mozilla/5.0 (Windows NT 6.1; rv:15.0) Gecko/20120716 Firefox/15.0a2",
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.8; rv:21.0) Gecko/20100101 Firefox/21.0",
            "Mozilla/5.0 (Windows NT 6.1; de;rv:12.0) Gecko/20120403211507 Firefox/12.0",
            "Mozilla/5.0 (Windows NT 6.1; rv:12.0) Gecko/20120403211507 Firefox/14.0.1",
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.6; rv:9.0) Gecko/20100101 Firefox/9.0",
            "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36",
            "Mozilla/5.0 (X11; OpenBSD i386) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.125 Safari/537.36",
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_3) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.1916.47 Safari/537.36",
            "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.517 Safari/537.36",
            "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.812.0 Safari/535.1",
            "Mozilla/5.0 (X11; Linux amd64) AppleWebKit/534.36 (KHTML, like Gecko) Chrome/13.0.766.0 Safari/534.36",
            "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/534.36 (KHTML, like Gecko) Chrome/13.0.766.0 Safari/534.36",
            "Mozilla/5.0 (compatible, MSIE 11, Windows NT 6.3; Trident/7.0; rv:11.0) like Gecko",
            "Opera/9.80 (X11; Linux i686; Ubuntu/14.10) Presto/2.12.388 Version/12.16",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36 Edge/12.246",
            "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; AS; rv:11.0) like Gecko",
            "Mozilla/5.0 (Windows; U; MSIE 9.0; WIndows NT 9.0; en-US)",
            "Mozilla/5.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; SLCC1; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET CLR 1.1.4322)",
            "Mozilla/4.0 (compatible; MSIE 6.01; Windows NT 6.0)",
            "Opera/9.80 (X11; Linux x86_64; U; en-GB) Presto/2.2.15 Version/10.01",
            "Opera/9.80 (Windows NT 5.1; U; ru) Presto/2.2.15 Version/10.00",
            "Opera/9.64(Windows NT 5.1; U; en) Presto/2.1.1",
            "Opera/10.60 (Windows NT 5.1; U; en-US) Presto/2.6.30 Version/10.60",
            "Opera/9.80 (J2ME/MIDP; Opera Mini/9.80 (J2ME/22.478; U; en) Presto/2.5.25 Version/10.54",
            "Opera/9.80 (Android; Opera Mini/7.6.35766/35.5706; U; en) Presto/2.8.119 Version/11.10",
            "Mozilla/5.0 (Android; Linux armv71; rv:5.0) Gecko/20110615 Fennec/5.0",
            "Mozilla/5.0 (Android; Linux armv7l; rv:9.0) Gecko/20111216 Firefox/9.0 Fennec/9.0",
            "Mozilla/5.0 (Macintosh; U; PPC Mac OS X Mach-O; en-US; rv:1.0.1) Gecko/20021220 Chimera/0.6",
            "Mozilla/5.0 (Windows; U; Windows NT 6.1; rv:2.2) Gecko/20110201",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:25.6) Gecko/20150723 PaleMoon/25.6.0",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:25.6) Gecko/20150723 Firefox/31.9 PaleMoon/25.6.0",
            "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:15.0) Gecko/20120819 Firefox/15.0 PaleMoon/15.0",
            "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.2.11) Gecko/20101023 Firefox/3.6.11 (Palemoon/3.6.11)"
        };

        private static string _currentUserAgent = string.Empty;

        public static string GetUserAgent(int index)
        {
            return (index < 0 || index >= _userAgents.Length) ? _userAgents[0] : _userAgents[index];
        }

        public static string GetRandomUserAgent()
        {
            var index = (new Random()).Next(0, _userAgents.Length);
            var randomUserAgent = GetUserAgent(index);

            return randomUserAgent;
        }

        public static string CurrentUserAgent
        {
            get
            {
                return GetUserAgent();
            }

            set
            {
                _currentUserAgent = value;
            }
        }

        protected static string GetUserAgent()
        {
            if (_currentUserAgent == string.Empty)
            {
                _currentUserAgent = GetRandomUserAgent();
            }

            return _currentUserAgent;
        }

    }
}
