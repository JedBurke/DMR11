using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DMR11;
using System.Drawing;

namespace DMR11_Tests
{
    [TestClass]
    public class SerializeManagerTests
    {
        [TestInitialize]
        public void Initialize()
        {
            CreateSettings();
        }

        public void CreateSettings()
        {
            var settings = new MainFormSettings();
            settings.FormLocation = new Point(50, 50);
            var manager = new MainFormSettingsManager(settings, "settings/window.json");
            manager.Save();
            manager.Dispose();
        }

        [TestMethod]
        public void Test_LoadSettings()
        {
            var manager = new MainFormSettingsManager(null, "settings/window.json");
            manager.Load();
            manager.Subject.FormLocation = new Point(150, 150);
            manager.Save();

            Assert.AreEqual(new Point(150, 150), manager.Subject.FormLocation);

        }
    }
}
