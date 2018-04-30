using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Bookmarks
{
    public interface IManager<T> : IDisposable
    {
        void Remove();

        void Save();
        void Save(string path);

        void Load(string path);

    }
}
