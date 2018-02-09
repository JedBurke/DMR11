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
        string bookmarksPath = "settings/bookmarks.json";
        BookmarkManager manager = null;

        [TestInitialize]
        public void SetupBookmarks()
        {
            manager = new BookmarkManager(bookmarksPath);

            if (manager.GetBookmarks().Length == 0)
            {
                Console.WriteLine("Bookmarks are empty...");

                manager.AddBookmark(new Bookmark()
                {
                    Name = "Grand Blue",
                    SeriesUri = new UriValidated("http://test.manga.com/grand_blue"),
                    SaveDestination = "D:/manga/grand_blue",
                    DateAdded = DateTime.Now.Ticks,
                    DateUpdated = DateTime.Now.Ticks,
                    Site = "test-manga"
                });


                manager.AddBookmark(new Bookmark()
                {
                    Name = "Crows",
                    SeriesUri = new UriValidated("http://test.manga.com/crows"),
                    SaveDestination = "D:/manga/crows",
                    DateAdded = DateTime.Now.Ticks,
                    DateUpdated = DateTime.Now.Ticks,
                    Site = "test-manga"
                });
            }
        }

        [TestMethod]
        public void LoadBookmarks()
        {
            foreach (var bookmark in manager.GetBookmarks())
            {
                Console.WriteLine(bookmark.Name);
            }
            
        }

        [TestMethod]
        public void SaveBookmarks()
        {
            manager.AddBookmark(new Bookmark()
            {
                Name = "Minamoto-kun Monogatari",
                SeriesUri = new UriValidated("http://test.manga.com/minamoto-kun_monogatari"),
                SaveDestination  = "D:/manga/minamoto-kun_monogatari",
                Site = "test-manga"
            });

            manager.Save();
        }

        [TestMethod]
        public void RemoveBookmark()
        {

        }

        [TestMethod]
        public void Test_Lookup()
        {
            Assert.IsNotNull(manager["Minamoto-kun Monogatari"]);
            Assert.IsNotNull(manager["minamoto-kun monogatari"]);

            Assert.AreEqual(manager["minamoto-kun monogatari"].SaveDestination, "D:/manga/minamoto-kun_monogatari", true);
        }

        [TestMethod]
        public void Test_IsBookmarked()
        {
           Assert.IsTrue(manager.IsBookmarked(new Uri("http://test.manga.com/minamoto-kun_monogatari")));
           Assert.IsTrue(manager.IsBookmarked(new Uri("https://test.manga.com/minamoto-kun_monogatari"), true));
           Assert.IsFalse(manager.IsBookmarked(new Uri("https://Test.Manga.Com/minamoto-kun_monogatari")));
        }
        
    }
}
