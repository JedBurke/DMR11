﻿using DMR11.Core.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DMR11
{
    [DataContract(Name="bookmark")]
    public class Bookmark : IBookmark
    {
        public Bookmark()
        {
            // Automatically sets the time when the bookmark was created.
            this.DateAdded = DateTime.UtcNow.Ticks;
            this.DateUpdated = DateTime.UtcNow.Ticks;
        }

        string _name = string.Empty;

        /// <summary>
        /// Sets the 'DateUpdated' property to the current time.
        /// </summary>
        public void UpdateBookmark()
        {
            this.DateUpdated = DateTime.UtcNow.Ticks;
        }

        [DataMember]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException();

                _name = value.Trim();
            }
        }

        [DataMember]
        public string Site
        {
            get;
            set;
        }

        [DataMember(Name="Uri")]
        public Uri SeriesUri
        {
            get;
            set;
        }


        [DataMember]
        public bool Completed
        {
            get;
            set;
        }

        [DataMember]
        public long DateAdded
        {
            get;
            set;
        }

        [DataMember]
        public long DateUpdated
        {
            get;
            set;
        }

        [DataMember(Name="Destination")]
        public string SaveDestination
        {
            get;
            set;
        }
    }
}
