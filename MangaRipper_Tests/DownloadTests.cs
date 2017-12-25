﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.IO;


namespace DMR11_Tests
{
    [TestClass]
    public class DownloadTest
    {
        [TestMethod]
        public void DownloadFileSenManga()
        {
            try
            {
                Uri address = new Uri("https://raw.senmanga.com/viewer/Minamoto-kun_Monogatari/239/2?token=12norzsrobp2l.22h1qvxtew5j");
                string fileName = "_test_file";

                if (File.Exists(fileName) == false)
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address);
                    request.Host = address.Host;
                    //request.Proxy = Proxy;
                    request.Credentials = CredentialCache.DefaultCredentials;
                    //request.Referer = Referrer ?? Address.AbsoluteUri;

                    CookieContainer c = new CookieContainer();
                    //c.Add(address, new Cookie("__cfduid", "dedfa0182c075ac863d587b27db70a7071513856559"));
                    c.Add(address, new Cookie("PHPSESSID", "klgvutthlnc9ni8fu0d0kvbis7"));

                    request.CookieContainer = c;


                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        //foreach (Cookie cookie in response.Cookies)
                        //{
                        //    request.CookieContainer.Add(cookie);
                        //}

                        using (Stream responseStream = response.GetResponseStream())
                        {
                            string tmpFileName = Path.GetTempFileName();

                            using (Stream strLocal = new FileStream(tmpFileName, FileMode.Create))
                            {
                                byte[] downBuffer = new byte[2048];
                                int bytesSize = 0;
                                while ((bytesSize = responseStream.Read(downBuffer, 0, downBuffer.Length)) > 0)
                                {
                                    //_cancellationToken.ThrowIfCancellationRequested();
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
                Console.WriteLine(ex);
                //string error = String.Format("{0} - Error while download: {2} - Reason: {3}", DateTime.Now.ToLongTimeString(), this.Name, address.AbsoluteUri, ex.Message);
                //throw new OperationCanceledException(error, ex);
            }
        }
    }
}