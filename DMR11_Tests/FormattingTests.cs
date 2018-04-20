using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DMR11_Tests
{
    [TestClass]
    public class FormattingTests
    {
        [TestMethod]
        public void Test_PrefixChapter()
        {
            /* Pseudo format:
             * {series_name} - {chapter:000.#}
             */

            var seriesName = "Houkago Play";
            var chapter = 3;

            // Transform the code with the keys into the expected format.
            var format = "{0} - {1:000.#}";

            Assert.AreEqual("Houkago Play - 003", string.Format(format, new object [] { seriesName, chapter }));
        }

        [TestMethod]
        public void Test_PrefixChapterDouble()
        {
            var seriesName = "Houkago Play";
            var chapter = 3.96f;
            
            var format = "{0} - {1:000.#######}";

            Assert.AreEqual("Houkago Play - 003.96", string.Format(format, seriesName, chapter));

        }
    }
}
