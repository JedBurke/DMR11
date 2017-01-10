using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Linq;
using System.Text.RegularExpressions;

namespace MangaRipper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //XDocument x = XDocument.Load(@"D:\domains.xml");
            //string domain = "http://www.mangafox.me/kissxsis/ch03";


            /*var c = from ele in x.Root.Elements("domain")
                    where Regex.IsMatch(domain, ele.Attribute("value").Value, RegexOptions.IgnoreCase)
                    select ele;*/

            /*XElement c = x.Root.Elements("domain").Single(
                ele => Regex.IsMatch(domain, ele.Attribute("value").Value, RegexOptions.IgnoreCase)
            );

            MessageBox.Show(c.ToString());*/
            
            //Console.WriteLine(!bool.Parse("+[]"));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());


        }
    }
}
