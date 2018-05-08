using System;
using DMR11.Core.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DMR11_Tests
{
    [TestClass]
    public class FileSystemTests
    {
        [TestMethod]
        public void Test_SafePath()
        {
            var unsafePath = @"D:\Manga\Fake:Path\Test???.jpg";
            var safePath = FileSystem.GetSafePath(unsafePath);

            Assert.AreEqual(@"D:\Manga\FakePath\Test.jpg", safePath);
        }

        [TestMethod]
        public void Test_IsPathValid()
        {
            var validPaths = new[]
            {
                @"D:\SomePath\pointingTonothing\file.jpg"
            };

            var invalidPaths = new[]
            {
                @"D:\SomePath\\pointingTo:nothing\.jpg"
            };

            foreach (var validPath in validPaths)
            {
                Assert.IsTrue(FileSystem.IsPathValid(validPath));
            }

            foreach (var invalidPath in invalidPaths)
            {
                Assert.IsFalse(FileSystem.IsPathValid(invalidPath));
            }
        }
    }
}
