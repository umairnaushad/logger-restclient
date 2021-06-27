using NUnit.Framework;
using System.Threading;

namespace GUINUnitTest
{
    [TestFixture]
    public class Tests
    {
        UIAutomationApp automationApp = new UIAutomationApp();

        [OneTimeSetUp]
        public void Setup()
        {
            automationApp.LaunchApplication();
            automationApp.GetMainWindow();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            automationApp.CloseApplication();
        }

        [Test]
        public void TestMainWindowTitle()
        {
            string expectedTest = "Rijks Museum";
            string actualtext = automationApp.GetMainWindowTitle();
            Assert.AreEqual(expectedTest, actualtext);
        }

        [Test, Order(1)]
        public void TestComboboxIsPopulated()
        {
            string expectedTest = "Paris-Artiste";
            string actualtext = automationApp.GetSelectedArtist();
            Assert.AreEqual(expectedTest, actualtext);
        }

        [Test, Order(2)]
        public void TestComboboxSelectValue()
        {
            string expectedTest = "Vincent van Gogh";
            automationApp.SetArtist("Vincent van Gogh");
            Thread.Sleep(1000);
            string actualtext = automationApp.GetSelectedArtist();
            Assert.AreEqual(expectedTest, actualtext);
        }

        [Test]
        public void TestLabelInstuction()
        {
            string expectedTest = "Double click on a picture to get more detail";
            string actualtext = automationApp.GetInstructions();
            Assert.AreEqual(expectedTest, actualtext);
        }
    }
}