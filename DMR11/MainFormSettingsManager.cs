using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace DMR11
{
    public class SerializationManager<T> : Bookmarks.IManager<T>
    {
        T _subject;
        string _path;
                
        public SerializationManager(T subject, string path)
        {
            this._subject = subject;
            this._path = path;
        }

        public T Subject
        {
            get
            {
                return _subject;
            }
        }

        public void Save()
        {
            Save(_subject, _path);
        }

        public void Save(string path)
        {
            Save(_subject, _path);
        }

        public void Save(T subject, string path)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(T));

                serializer.WriteObject(stream, subject);

                if (!File.Exists(path))
                {
                    var pathDirectory = Path.GetDirectoryName(path);

                    if (!Directory.Exists(pathDirectory))
                    {
                        Directory.CreateDirectory(pathDirectory);
                    }

                }

                File.WriteAllBytes(path, stream.ToArray());
            }
        }

        public void Load()
        {
            Load(_path);
        }

        public void Load(string path)
        {
            T serializedObject = default(T);

            if (File.Exists(path))
            {
                var subjectFile = File.ReadAllBytes(path);

                using (var stream = new MemoryStream(subjectFile))
                {
                    var serializer = new DataContractJsonSerializer(typeof(T));

                    serializedObject = (T)serializer.ReadObject(stream);
                }
            }
            else
            {
                throw new FileNotFoundException();
            }

            if (string.IsNullOrWhiteSpace(_path))
            {
                _path = path;
            }

            _subject = serializedObject;
        }

        public void Dispose()
        {
            _subject = default(T);
            _path = null;
        }

    }

    public class MainFormSettingsManager : SerializationManager<MainFormSettings>
    {
        public MainFormSettingsManager(string path)
            : base(new MainFormSettings(), path)
        {
        }

        public MainFormSettingsManager(MainFormSettings settings, string path)
            : base(settings, path)
        {
        }
        
    }
}
