using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using DMR11.Properties;
using System.Security;

namespace DMR11
{
    static class Option
    {
        /// <summary>
        /// Get proxy setting
        /// </summary>
        /// <returns></returns>
        public static IWebProxy GetProxy()
        {
            IWebProxy wp = null;

            // Todo: Implement.
            //if (MainFormSettings.Default.ProxyEnable == true)
            //{
            //    try
            //    {
            //        string host = MainFormSettings.Default.ProxyHost;
            //        int port = Convert.ToInt32(MainFormSettings.Default.ProxyPort);
            //        string userName = MainFormSettings.Default.ProxyUserName;
            //        string password = MainFormSettings.Default.ProxyPassword;

            //        wp = new WebProxy(host, port);
            //        wp.Credentials = new NetworkCredential(userName, password);
            //    }
            //    catch
            //    {
            //        wp = null;
            //    }
            //}

            return wp;
        }
    }
}
