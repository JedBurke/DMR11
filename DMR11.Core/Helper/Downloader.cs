using CloudFlareUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DMR11.Core.Helper
{
    public class Downloader
    {
        private static readonly Lazy<Downloader> lazy = new Lazy<Downloader>(() => new Downloader());

        public static Downloader Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        string UserAgent;

        private Downloader()
        {
            //UserAgent = "Mozilla/5.0 (X11; Linux x86_64; rv:28.0) Gecko/20100101 Firefox/28.0";
            UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:40.0) Gecko/20100101 Firefox/40.1";
        }

        public string DownloadString(UriValidated address)
        {
            StringBuilder result = new StringBuilder();
            
            try
            {
                try
                {
                    HttpClientHandler handler = null;
                    ClearanceHandler handlerKiss = null;
                    HttpClient client = null;
                    
                    if (string.Compare(address.Host, "kissmanga.com") == 0)
                    {
                        Console.WriteLine("Host is KissManga.");

                        handlerKiss = new ClearanceHandler();

                        // Create a HttpClient that uses the handler to bypass CloudFlare's JavaScript challange.
                        client = new HttpClient(handlerKiss)
                        {
                            Timeout = TimeSpan.FromSeconds(15)
                        };



                    }
                    else
                    {
                        handler = new HttpClientHandler()
                        {
                            AutomaticDecompression = DecompressionMethods.GZip,
                            Credentials = CredentialCache.DefaultNetworkCredentials,
                            CookieContainer = cookieContainer ?? (cookieContainer = new CookieContainer()),
                            UseCookies = true
                        };

                        client = new HttpClient(handler)
                        {
                            Timeout = TimeSpan.FromSeconds(15)
                        };

                        client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
                        client.DefaultRequestHeaders.Add("Host", address.Host);
                        client.DefaultRequestHeaders.Add("Referer", address.Host);
                        
                        //client.DefaultRequestHeaders.Add("Referer", address.Host);
                        //client.DefaultRequestHeaders.Add("User-Agent", UserAgents[UserAgentIndex]);
                    }

                    var content = client.GetStringAsync(address.ToString()).Result;

                    result.Append(content);


                }
                catch (WebException webEx)
                {
                    Console.WriteLine(webEx.ToString());
                }
                catch (Exception)
                {

                    throw;
                }

                return result.ToString();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                string error = String.Format("{0} - Error while download: {2} - Reason: {3}", DateTime.Now.ToLongTimeString(), "XX", address.AbsoluteUri, ex.Message);
                throw new Exception(error, ex);
            }
        }

        private CookieContainer cookieContainer = null;

        private HttpWebRequest MakeRequest(UriValidated address)
        {
            if (cookieContainer == null)
            {
                cookieContainer = new CookieContainer();
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address);
            request.Host = address.Host;
            //request.Proxy = Proxy;
            request.Credentials = CredentialCache.DefaultNetworkCredentials;
            //request.Accept = "gzip, deflate";
            request.Accept = "*/*";
            //request.Referer = Referrer ?? Address.AbsoluteUri;
            request.Referer = address.AbsoluteUri;

            request.UserAgent = UserAgent;

            request.CookieContainer = cookieContainer;

            Console.WriteLine(string.Concat(DateTime.Now.ToLongDateString(), " ", DateTime.Now.ToLongTimeString()));

            return request;
        }

        public void DownloadFile(UriValidated address, string fileName, CancellationToken token)
        {
            try
            {
                if (File.Exists(fileName) == false)
                {
                   HttpWebRequest request = MakeRequest(address);

                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            string tmpFileName = Path.GetTempFileName();

                            using (Stream strLocal = new FileStream(tmpFileName, FileMode.Create))
                            {
                                byte[] downBuffer = new byte[2048];
                                int bytesSize = 0;
                                while ((bytesSize = responseStream.Read(downBuffer, 0, downBuffer.Length)) > 0)
                                {
                                    if (token != null)
                                    {
                                        token.ThrowIfCancellationRequested();
                                    }

                                    strLocal.Write(downBuffer, 0, bytesSize);
                                }

                            }

                            File.Move(tmpFileName, fileName);
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                string error = String.Format("{0} - Error while download: {2} - Reason: {3}", DateTime.Now.ToLongTimeString(), "xx", address.AbsoluteUri, ex.Message);
                throw new OperationCanceledException(error, ex);
            }
        }

        public void SetUserAgent(string userAgent)
        {
        }

        public void ClearCookies()
        {
        }

    }
}
