using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;

namespace DMR11_Tests
{
    [TestClass]
    public class Test_CloudFlare
    {
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


    }


}
