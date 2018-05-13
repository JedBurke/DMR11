using DMR11;
using DMR11.Core.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace DMR11_Tests
{
    [TestClass]
    public class BookmarkTest
    {
        string bookmarksPath = "settings/bookmarks.json";
        BookmarkManager manager = null;

        [TestInitialize]
        public void Test_Initialize()
        {
            manager = new BookmarkManager(bookmarksPath);
            
            manager.AddBookmark(new Bookmark()
            {
                Name = "Grand Blue",
                SeriesUri = new ValidatedUri("http://test.manga.com/grand_blue"),
                SaveDestination = "D:/manga/grand_blue",
                DateAdded = DateTime.Now.Ticks,
                DateUpdated = DateTime.Now.Ticks,
                Site = "test-manga"
            });


            manager.AddBookmark(new Bookmark()
            {
                Name = "Crows",
                SeriesUri = new ValidatedUri("http://test.manga.com/crows"),
                SaveDestination = "D:/manga/crows",
                DateAdded = DateTime.Now.Ticks,
                DateUpdated = DateTime.Now.Ticks,
                Site = "test-manga"
            });

            manager.AddBookmark(new Bookmark()
            {
                Name = "Minamoto-kun Monogatari",
                SeriesUri = new ValidatedUri("http://test.manga.com/minamoto-kun_monogatari"),
                SaveDestination = "D:/manga/minamoto-kun_monogatari",
                Site = "test-manga"
            });

        }

        [TestMethod]
        public void Test_GetBookmark()
        {
            Assert.IsNotNull(manager["Minamoto-kun Monogatari"]);
            Assert.IsNotNull(manager["minamoto-kun monogatari"]);

            Assert.AreEqual(manager["minamoto-kun monogatari"].SaveDestination, "D:/manga/minamoto-kun_monogatari", true);
        }

        [TestMethod]
        public void Test_GetBookmarks()
        {
            foreach (var bookmark in manager.GetBookmarks())
            {
                Console.WriteLine("Series: {0}", bookmark.Name);
            }

        }

        [TestMethod]
        public void Test_AddDuplicate()
        {
            var minamoto = new Bookmark()
            {
                Name = "Minamoto-kun Monogatari",
                SeriesUri = new ValidatedUri("http://test.manga.com/minamoto-kun_monogatari"),
                SaveDestination = "D:/manga/minamoto-kun_monogatari",
                Site = "test-manga"
            };
            
            Assert.AreEqual(manager.AddBookmark(minamoto), AddBookmarkStatus.Duplicate);
        }

        [TestMethod]
        public void Test_GetBookmark_with_URI()
        {
            Assert.IsNull(manager.GetBookmarkByUri(new ValidatedUri("https://test.manga.com/minamoto-kun_monogatari")));
            Assert.IsNotNull(manager.GetBookmarkByUri(new ValidatedUri("http://test.manga.com/minamoto-kun_monogatari")));
            
            // Checks if the series is a match without its scheme.
            Assert.IsNotNull(manager.GetBookmarkByUri(new ValidatedUri("https://test.manga.com/minamoto-kun_monogatari"), false));
        }

        [TestMethod]
        public void Test_AddSuccess()
        {
            var minamoto = new Bookmark()
            {
                Name = "Minamoto-kun Monogatari",
                SeriesUri = new ValidatedUri("http://test.manga.com/minamoto-kun_monogatari"),
                SaveDestination = "D:/manga/minamoto-kun_monogatari",
                Site = "test-manga"
            };

            manager.RemoveBookmark(minamoto);

            Assert.AreEqual(manager.AddBookmark(minamoto), AddBookmarkStatus.Success);
        }

        [TestMethod]
        public void Test_SaveBookmarks()
        {
            manager.Save();
        }

        [TestMethod]
        public void Test_RemoveBookmark()
        {
            Assert.IsTrue(manager.IsBookmarked("minamoto-kun monogatari"));
            Assert.IsTrue(manager.RemoveBookmark("minamoto-kun monogatari"));

            manager.Save();
        }

        [TestMethod]
        public void Test_IsBookmarked()
        {
            Assert.IsTrue(manager.IsBookmarked("Minamoto-kun Monogatari   "));

            Assert.IsTrue(manager.IsBookmarked(new ValidatedUri("http://test.manga.com/minamoto-kun_monogatari")));
            Assert.IsTrue(manager.IsBookmarked(new ValidatedUri("https://test.manga.com/minamoto-kun_monogatari"), true));
            Assert.IsFalse(manager.IsBookmarked(new ValidatedUri("https://Test.Manga.Com/minamoto-kun_monogatari")));
        }

        [TestCleanup]
        public void Test_Cleanup()
        {
            manager.Dispose();
        }

    }
}
