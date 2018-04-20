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
                    TitleCaseExclusions.EnglishAndJapanese
                )
            );

            Assert.AreEqual(
                "Brighton on Sea",
                WordConverter.ToTitleCase(
                    "brighton on sea",
                    TitleCaseExclusions.EnglishAndJapanese
                )
            );

            Assert.AreEqual(
                "The Last of the Mohicans",
                WordConverter.ToTitleCase(
                    "the last of the mohicans",
                    TitleCaseExclusions.EnglishAndJapanese
                )
            );

            Assert.AreEqual(
                "Ryū ga Gotoku",
                WordConverter.ToTitleCase(
                    "RYŪ GA GOTOKU",
                    TitleCaseExclusions.EnglishAndJapanese
                )
            );

            Assert.AreEqual(
                "Bungaku Shōjo ni Tagari no Douke",
                WordConverter.ToTitleCase(
                    "bungaku shōjo ni tagari no douke",
                    TitleCaseExclusions.EnglishAndJapanese
                )
            );

            // Todo: Include ignore option.
            // The current test is failing due to the 'O' being capitalized as per English rules.
            // A solution could include invoking different rules based on the exclusions 
            // passed to the function. If the English optio is passed, the function follows English
            // rules. If the Japanese option is passed, it follows Japanese rules.
            
            Assert.AreEqual(
                 "Kono Subarashii Sekai ni Shukufuku o!",
                 WordConverter.ToTitleCase(                    
                     WordConverter.ToTitleCase(
                        "kono subarashii sekai Ni Shukufuku O!",
                        TitleCaseExclusions.English
                     ),
                    TitleCaseExclusions.Japanese
                 )
            );
        }
    }
}
