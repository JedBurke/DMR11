using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

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
            LoadedPath = path;

        }

        public bool AddBookmark(IBookmark bookmark)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            return Save(this._loadedPath);
        }

        public bool Save(string path)
        {
            throw new NotImplementedException();
        }

        public BookmarkManager()
        {
        }

        public BookmarkManager(string path)
            : this()
        {
            LoadBookmarks(path);
        }

        
        List<IBookmark> _bookmarks = null;

        List<IBookmark> Bookmarks
        {
            get
            {
                return _bookmarks;
            }
        }


    }
}
