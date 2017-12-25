using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Core
{
    public interface IParseDetails<T> : ICommand
    {
        string XPath { get; set; }
        string AttributeName { get; set; }
        Func<HtmlAgilityPack.HtmlNode, IParseDetails<T>, T> ParseAction { get; set; }
    }
}
