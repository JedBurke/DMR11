using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;

namespace DMR11
{
    public class BookmarkManager
    {
        private string _loadedPath;
        public string LoadedPath
        {
            get
            {
                return this._loadedPath;
            }
            private set
            {
                this._loadedPath = value;
            }
        }
        
        public void LoadBookmarks()
        {
        }

        public void LoadBookmarks(string path)
        {
            if (File.Exists(path))
            {
                LoadedPath = path;

                var bookmarksFile = File.ReadAllBytes(LoadedPath);

                using (var stream = new MemoryStream(bookmarksFile))
                {
                    var serializer = new DataContractJsonSerializer(typeof(Bookmark[]));

                    Bookmarks.AddRange((Bookmark[])serializer.ReadObject(stream));
                }

            }
            else
            {
                throw new FileNotFoundException();
            }

        }

        public AddBookmarkStatus AddBookmark(Bookmark bookmark)
        {
            if (!IsBookmarked(bookmark.SeriesUri))
            {
                Bookmarks.Add(bookmark);

                return AddBookmarkStatus.Success;
            }
            else
            {
                return AddBookmarkStatus.Duplicate;
            }
        }


        public void Save()
        {
            Save(this._loadedPath);
        }

        public void Save(string path)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(Bookmark[]));

                serializer.WriteObject(stream, Bookmarks.ToArray());

                if (!File.Exists(path))
                {
                    var pathDirectory = Path.GetDirectoryName(path);

                    if (!Directory.Exists(pathDirectory))
                        Directory.CreateDirectory(pathDirectory);

                }

                File.WriteAllBytes(path, stream.ToArray());
            }
        }

        public BookmarkManager()
        {
            Bookmarks = new List<Bookmark>();
        }

        public BookmarkManager(string path)
            : this()
        {
            LoadBookmarks(path);
        }

        
        List<Bookmark> Bookmarks = null;
        
        /// <summary>
        /// Returns a the list of bookmarked series.
        /// </summary>
        /// <returns></returns>
        public Bookmark[] GetBookmarks()
        {
            return Bookmarks.ToArray();
        }

        /// <summary>
        /// Returns whether the input series name matches that of a bookmarked series. The look-up is done
        /// in a case-insensitive manner with the input string stripped of its opening and closing whitespace.
        /// </summary>
        /// <param name="seriesName">The series which to search.</param>
        /// <returns>A boolean value of whether the series has been bookmarked.</returns>
        public bool IsBookmarked(string seriesName)
        {
            if (Bookmarks.Count == 0)
                return false;

            if (string.IsNullOrWhiteSpace(seriesName))
                throw new ArgumentNullException();
            
            seriesName = seriesName.Trim();

            return Bookmarks.Any(bookmark => string.Compare(bookmark.Name, seriesName, true) == 0);
        }

        /// <summary>
        /// Returns whether the input URI matches a bookmarked series.
        /// </summary>
        /// <param name="seriesUri">The series which to compare to the list of bookmarked series.</param>
        /// <param name="invariant">Whether to disregard the scheme and treat HTTP as HTTPS and vice-versa.</param>
        /// <returns>A boolean value of whether the series has been bookmarked.</returns>
        public bool IsBookmarked(Uri seriesUri, bool invariant = false)
        {
            return invariant
                    ? Bookmarks.Any(bookmark => string.Compare(StripUriScheme(bookmark.SeriesUri), StripUriScheme(seriesUri), true) == 0)
                    : Bookmarks.Any(bookmark => bookmark.SeriesUri == seriesUri);
        }

        /// <summary>
        /// Removes the scheme of the input URI.
        /// </summary>
        /// <param name="uri">The URI object which to have its scheme removed.</param>
        /// <returns>A string representation of the URI with its scheme removed.</returns>
        private string StripUriScheme(Uri uri)
        {
            var uriStringRep = uri.ToString();

            if (uri.Scheme != string.Empty)
            {
                uriStringRep = uriStringRep.Remove(0, uri.Scheme.Length);

                if (uriStringRep.StartsWith("://"))
                    uriStringRep = uriStringRep.Remove(0, 3);
            }

            return uriStringRep;
        }

        public Bookmark LookupBookmark(string seriesName)
        {
            return Bookmarks.FirstOrDefault(bookmark => string.Compare(bookmark.Name, seriesName, true) == 0);
        }

        public Bookmark this[string seriesName]
        {
            get
            {
                return LookupBookmark(seriesName);
            }
        }

        public Bookmark this[Bookmark bookmark]
        {
            set
            {
                AddBookmark(value);
            }
        }

    }
}
