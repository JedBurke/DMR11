﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DMR11.Core.Service
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
            UserAgent = Service.UserAgent.CurrentUserAgent;
        }

        public string DownloadString(Uri address)
        {
            StringBuilder result = new StringBuilder();

            try
            {
                try
                {                    
                    var handler = new HttpClientHandler()
                    {
                        AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                        Credentials = CredentialCache.DefaultNetworkCredentials,
                        CookieContainer = cookieContainer ?? (cookieContainer = new CookieContainer()),
                        UseCookies = true
                    };

                    if (Net.ProxyServer.Instance.UseProxyServer)
                    {
                        handler.UseProxy = true;
                        handler.Proxy = Net.ProxyServer.Instance.Proxy;
                    }

                    var client = new HttpClient(handler)
                    {
                        Timeout = TimeSpan.FromSeconds(15)
                    };

                    client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
                    client.DefaultRequestHeaders.Add("Host", address.Host);
                    client.DefaultRequestHeaders.Add("Referer", address.Host);

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

        private HttpWebRequest MakeRequest(Uri address)
        {
            if (cookieContainer == null)
            {
                cookieContainer = new CookieContainer();
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address);
            request.Host = address.Host;
            request.Credentials = CredentialCache.DefaultNetworkCredentials;
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            request.Accept = "*/*";
            request.Referer = address.AbsoluteUri;
            request.Headers[HttpRequestHeader.AcceptEncoding] = "gzip, deflate";

            request.UserAgent = UserAgent;

            request.CookieContainer = cookieContainer;

            if (Net.ProxyServer.Instance.UseProxyServer)
            {
                request.Proxy = Net.ProxyServer.Instance.Proxy;
            }

            Console.WriteLine(string.Concat(DateTime.Now.ToLongDateString(), " ", DateTime.Now.ToLongTimeString()));

            return request;
        }

        public void DownloadFile(Uri address, string fileName, CancellationToken token)
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
            Service.UserAgent.CurrentUserAgent = userAgent;
        }

        public void ClearCookies()
        {
        }

    }
}
