using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Bookmarks
{
    public interface IManager<T> : IDisposable
    {
        void Save();
        void Save(T subject, string path);

        void Load();
        void Load(string path);

        T Subject { get; }
    }

    public enum IOStatus : int
    {
        Success,
        Failed,
        FileNotFound,
        AccessDenied
    }
}
