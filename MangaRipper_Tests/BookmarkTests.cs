using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using DMR11;

namespace DMR11_Tests
{
    [TestClass]
    public class BookmarkTest
    {
        [TestMethod]
        public void SerializeBookmarks()
        {
            var bookmarkManager = new DMR11.BookmarkManager();
            bookmarkManager.LoadBookmarks("settings/bookmarks.xml");

            //var bookmarks = new Bookmarks();
            //bookmarks.BookmarkedSeries = new[] {
            //    new Bookmark() {
            //        Name = "Grand Blue",
            //        SeriesUri = new UriValidated("//mangafox.la/manga/grand_blue"),
            //        Completed = false
            //    },
            //    new Bookmark() {
            //        Name = "Crows"
            //    }
            //};

            //var bookmark = new DMR11.Bookmark();
            //bookmark.Name = "Grand Blue";
            //bookmark.SeriesUri = new UriValidated("//mangafox.la/manga/grand_blue");

            var i = new Bookmark[] {
                new Bookmark() {
                    Name = "Grand Blue",
                    SeriesUri = new UriValidated("//mangafox.la/manga/grand_blue"),
                    Completed = false
                },
                new Bookmark() {
                    Name = "Crows"
                }
            };


            var stream1 = new MemoryStream();
            var xmlSer = new DataContractJsonSerializer(typeof(Bookmark[]));
                        
            xmlSer.WriteObject(stream1, i);

            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);

            Console.WriteLine("Bookmark");
            //Console.WriteLine(sr.ReadToEnd());

            File.WriteAllText(DateTime.Now.Ticks + ".json", sr.ReadToEnd());

        }

        [TestMethod]
        public void DeserializeBookmarks()
        {
            var bookmarkManager = new DMR11.BookmarkManager();
            //bookmarkManager.LoadBookmarks("settings/bookmarks.xml");
            var file = System.IO.File.ReadAllBytes("settings/bookmarks.json");

            var stream1 = new MemoryStream(file);

            var xmlSer = new DataContractJsonSerializer(typeof(DMR11.Bookmark[]));

            object i = xmlSer.ReadObject(stream1);


            Console.WriteLine("Strop");
        }

        [DataContract(Name = "bookmarks")]
        public class Bookmarks
        {
            [DataMember]
            public Bookmark[] BookmarkedSeries;
        }

    }
}
