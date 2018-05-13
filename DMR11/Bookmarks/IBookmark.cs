using DMR11.Core.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DMR11
{
    public interface IBookmark
    {
        string Name { get; set; }
        string Site { get; set; }
        Uri SeriesUri { get; set; }
        string SaveDestination { get; set; }
        bool Completed { get; set; }
        long DateAdded { get; set; }
        long DateUpdated { get; set; }
    }
}
