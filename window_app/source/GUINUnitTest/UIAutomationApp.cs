using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Automation;

namespace GUINUnitTest
{
    class UIAutomationApp
    {
        private string applicationName = "RijksMuseumApplication.exe";
        AutomationElement mainWindow;

        public void LaunchApplication()
        {
            string applicationPath2 = Directory.GetCurrentDirectory() + "\\" + applicationName;
            string applicationPath = Directory.GetParent(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                ).FullName + "\\" + applicationName; 
            Process p = Process.Start(applicationPath);
        }

        public string GetMainWindowTitle()
        {
            GetMainWindow();

            if (mainWindow == null)
                return "Failed to find main window";
            else
                return mainWindow.Current.Name;
        }

        public void CloseApplication()
        {

        }

        private void GetMainWindow()
        {
            int numWaits = 0;
            do
            {
                mainWindow = AutomationElement.RootElement.FindFirst(TreeScope.Children,
                  new PropertyCondition(AutomationElement.NameProperty, "Rijks Museum"));

                ++numWaits;
                Thread.Sleep(100);
            } while (mainWindow == null && numWaits < 50);
        }
    }
}
