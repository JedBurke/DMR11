using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DMR11.Core;
using DMR11.Core.Net;
using System.Net;
using System.IO;


namespace DMR11_Tests
{
    [TestClass]
    public class NetworkTests
    {
        [TestMethod]
        public void Test_ValidatedUri_Constructor()
        {
            Assert.AreEqual(
                "http://www.fanfox.net/manga/name/vol_5_ch_15/1.htm",
                new ValidatedUri("//www.fanfox.net/manga/name/vol_5_ch_15/1.htm").ToString()
            );

            Assert.AreEqual(
                "http://www.fanfox.net/manga/name/vol_5_ch_15/1.htm",
                new ValidatedUri("http://www.fanfox.net/manga/name/vol_5_ch_15/1.htm").ToString()
            );
            
            Assert.AreNotEqual(
                "https://www.fanfox.net/manga/name/vol_5_ch_15/1.htm",
                new ValidatedUri("//www.fanfox.net/manga/name/vol_5_ch_15/1.htm").ToString()
            );

            Assert.AreEqual(
                "https://www.fanfox.net/manga/name/vol_5_ch_15/1.htm",
                new ValidatedUri("//www.fanfox.net/manga/name/vol_5_ch_15/1.htm", UriScheme.Https).ToString()
            );
        }

    }
}
