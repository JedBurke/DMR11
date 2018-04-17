using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;

namespace DMR11_Tests
{
    [TestClass]
    public class Test_CloudFlare
    {
        [TestMethod]
        public void Test_StatusRedirectHandler()
        {
            var handler = new DMR11.Core.StatusRedirectionHandler();
            var client = new HttpClient(handler);

            var address = "http://kissmanga.com/Manga/Gomen-ne-Money/";
            var result = string.Empty;

            result = client.GetAsync(address).Result.Content.ReadAsStringAsync().Result;

            Console.WriteLine(result);
        }

        [TestMethod]
        public void Test_EnterKissManga()
        {
            string address = "http://kissmanga.com/Manga/Gomen-ne-Money/";

            CloudFlareUtilities.ClearanceHandler handler = new CloudFlareUtilities.ClearanceHandler();

            HttpClient client = new HttpClient(handler);
            client.Timeout = TimeSpan.FromSeconds(25);
            string html = null;

            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");

            try
            {
                html = client.GetStringAsync(address).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine(html);
        }

        [TestMethod]
        public void Test_EnterKissMangaCustom()
        {
            string address = "http://kissmanga.com/Manga/Gomen-ne-Money/";
            string result = "";

            var handler = new CloudFlareUtilities.ClearanceHandler(new DMR11.Core.StatusRedirectionHandler());


            var client = new HttpClient(handler);

            client.Timeout = TimeSpan.FromSeconds(25);

            result = client.GetStringAsync(address).Result;
            

            Console.WriteLine(result);

        }


    }


}
