using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMR11.Core
{
    public class ChapterProgress
    {
        public ChapterProgress(IChapter chapter, int percent)
        {
            Chapter = chapter;
            Percent = percent;
        }
        public IChapter Chapter
        {
            get;
            private set;
        }

        public int Percent
        {
            get;
            private set;
        }
    }
}
