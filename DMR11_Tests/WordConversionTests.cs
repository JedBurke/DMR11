using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DMR11.Core.Helper;

namespace DMR11_Tests
{
    [TestClass]
    public class WordConversionTests
    {
        [TestMethod]
        public void Test_TitleCase()
        {
            Assert.AreEqual(
                "Snow White and the Seven Dwarfs",
                WordConverter.ToTitleCase(
                    "snow white and the seven dwarfs",
                    WordConverter.ExclusionsEng
                )
            );

            Assert.AreEqual(
                "Brighton on Sea",
                WordConverter.ToTitleCase(
                    "brighton on sea",
                    WordConverter.ExclusionsEng
                )
            );

            Assert.AreEqual(
                "The Last of the Mohicans",
                WordConverter.ToTitleCase(
                    "the last of the mohicans",
                    WordConverter.ExclusionsEng
                )
            );
        }
    }
}
