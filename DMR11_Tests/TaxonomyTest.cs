using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DMR11_Tests
{
    [TestClass]
    public class TaxonomyTest
    {
        [TestInitialize]
        public void Startup()
        {
            
        }

        [TestMethod]
        public void TestTaxonomy()
        {
            DMR11.Core.HostTaxonomy ht = new DMR11.Core.HostTaxonomy();


            var hostAddress = new UriValidated("https://sub.fanfox.net/manga/");

            var i = hostAddress.Host;
            var ind = i.IndexOf('.');

            Assert.AreEqual("fanfox", i.Substring(ind + 1, i.IndexOf('.', ind) + ind));

            //ht.GetDirectory(hostAddress);
            
        }

        [TestCleanup]
        public void Cleanup()
        {

        }
    }
}
