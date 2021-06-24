using NUnit.Framework;

namespace GUINUnitTest
{
    public class Tests
    {
        UIAutomationApp obj = new UIAutomationApp();

        [SetUp]
        public void Setup()
        {
            //obj.LaunchApplication();
        }

        [Test]
        public void TestMainWindowTitle()
        {
            string expectedTest = "Rijks Museum";
            string actualtext = obj.GetMainWindowTitle();
            Assert.AreEqual(expectedTest, actualtext);
        }
    }
}