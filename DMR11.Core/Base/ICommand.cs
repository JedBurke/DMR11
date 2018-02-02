using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Core
{
    public interface ICommand
    {
        NLog.ILogger Logger { get; set; }
    }
}
