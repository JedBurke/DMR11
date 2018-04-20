using DMR11.Core;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DMR11_Tests
{
    [TestClass]
    public class WebsiteHostSerializationTests
    {
        [TestMethod]
        public void Test_SerializeMangaFox()
        {
            var serializer = new DMR11.Core.WebsiteHost.IniWebsiteHostSerializer();
            var host = serializer.SeralizeFromPath("hosts/fanfox/config.ini");

            Assert.IsNotNull(host);

            Assert.AreEqual("//div[@class=\"cover\"]/img", host.Title.Path);
            Assert.AreEqual("alt", host.Title.Value);
            
            Assert.AreEqual("//a[contains(@class, 'tips')]", host.Chapters.Path);
            Assert.AreEqual("href", host.Chapters.Value);

            Assert.AreEqual("//select[@class='m'][1]/option", host.Pages.Path);
            Assert.AreEqual("value", host.Pages.Value);
            Assert.AreEqual("(?<page>\\d+)", host.Pages.ParseRegex);
            Assert.AreEqual("$(address_trimmed)/$(regex__page).html", host.Pages.ParseReplace);

            Assert.AreEqual("//img[@id='image']", host.Page.Path);
            Assert.AreEqual("src", host.Page.Value);

        }
    }
}
