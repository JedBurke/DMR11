using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMR11
{
    public interface IMainFormSettings
    {
        string SaveTo { get; set; }
        UriValidated Url { get; set; }
        string ProxyHost { get; set; }
        decimal ProxyPort { get; set; }
        string ProxyUserName { get; set; }
        string ProxyPassword { get; set; }
        bool ProxyEnable { get; set; }
        Size FormSize { get; set; }
        Point FormLocation { get; set; }
        FormWindowState State { get; set; }
        string DefaultSaveDestination { get; set; }
    }
    
}
