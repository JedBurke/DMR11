using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DMR11
{
    [DataContract(Name="settings")]
    public class MainFormSettings : IMainFormSettings
    {
        [DataMember]
        public string SaveTo
        {
            get;
            set;
        }

        [DataMember]
        public string Url
        {
            get;
            set;
        }

        [DataMember]
        public string ProxyHost
        {
            get;
            set;
        }

        [DataMember]
        public decimal ProxyPort
        {
            get;
            set;
        }

        [DataMember]
        public string ProxyUserName
        {
            get;
            set;
        }

        [DataMember]
        public string ProxyPassword
        {
            get;
            set;
        }

        [DataMember]
        public bool ProxyEnable
        {
            get;
            set;
        }

        [DataMember]
        public Size FormSize
        {
            get;
            set;
        }

        [DataMember]
        public Point FormLocation
        {
            get;
            set;
        }

        [DataMember]
        public FormWindowState State
        {
            get;
            set;
        }

        [DataMember]
        public string DefaultSaveDestination
        {
            get;
            set;
        }
    }
}
