using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Automation;

namespace GUINUnitTest
{
    class UIAutomationApp
    {
        private string applicationName = "RijksMuseumApplication.exe";
        AutomationElement mainWindow;

        [PermissionSetAttribute(SecurityAction.LinkDemand, Name = "FullTrust")]
        public void LaunchApplication()
        {
            CloseApplication();
            string applicationPath = Directory.GetParent(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                ).FullName + "\\" + "StartApplication.bat";// applicationName;
            Console.WriteLine("############################");
            Console.WriteLine(applicationPath);
            Process p = Process.Start(applicationPath);
        }

        public void CloseApplication()
        {
            string name = applicationName.Remove(applicationName.Length - 4, 4);
            Process[] processArray = Process.GetProcessesByName(name);

            if (processArray.Length != 0)
            {
                for (int i = 0; i < processArray.Length; i++)
                {
                    processArray[i].Kill();
                    Thread.Sleep(100);
                }
            }
        }

        public string GetMainWindowTitle()
        {
            if (mainWindow == null)
                return "Failed to find main window";
            else
                return mainWindow.Current.Name;
        }

        public string GetSelectedArtist()
        {
            AutomationElement cb = FindControlByAutomationId(ControlType.ComboBox, "cb_ArtistName");
            return ComboBoxGetSelectedValue(cb);
        }

        public void SetArtist(string artistName)
        {
            AutomationElement cb = FindControlByAutomationId(ControlType.ComboBox, "cb_ArtistName");
            ComboBoxSelectValue(cb, artistName);
        }

        public string GetInstructions()
        {
            AutomationElement label = FindControlByAutomationId(ControlType.Text, "lb_Instructions");
            return label.Current.Name;
        }

        public void GetMainWindow()
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

        private AutomationElement FindControlByAutomationId(ControlType controlType, string controlName)
        {
            PropertyCondition searchByControlType = new PropertyCondition(AutomationElement.ControlTypeProperty, controlType);
            PropertyCondition searchByName = new PropertyCondition(AutomationElement.AutomationIdProperty, controlName);
            AndCondition searchConditions = new AndCondition(searchByControlType, searchByName);
            return mainWindow.FindFirst(TreeScope.Children, searchConditions);
        }

        private string ComboBoxGetSelectedValue(AutomationElement currentElement)
        {
            // Combo box needs to be expanded in order to read selected value
            ExpandCollapsePattern expandPattern = (ExpandCollapsePattern)currentElement.GetCurrentPattern(ExpandCollapsePattern.Pattern);
            expandPattern.Expand();
            PropertyCondition searchByControlType = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ListItem);
            AutomationElementCollection listItemCollection = currentElement.FindAll(TreeScope.Descendants, searchByControlType);

            foreach (AutomationElement listItem in listItemCollection)
            {
                SelectionItemPattern selecteditem =
                    listItem.GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern;
                if (selecteditem.Current.IsSelected == true)
                {
                    return listItem.Current.Name.ToString();
                }
            }

            return "";
        }

        private void ComboBoxSelectValue(AutomationElement currentElement, string valueToSelect)
        {
            // Combo box needs to be expanded in order to read selected value
            ExpandCollapsePattern expandPattern = (ExpandCollapsePattern)currentElement.GetCurrentPattern(ExpandCollapsePattern.Pattern);
            expandPattern.Expand();
            PropertyCondition searchByControlType = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ListItem);
            PropertyCondition searchByName = new PropertyCondition(AutomationElement.NameProperty, valueToSelect);
            AndCondition searchConditions = new AndCondition(searchByControlType, searchByName);
            AutomationElement listItem= currentElement.FindFirst(TreeScope.Descendants, searchConditions);

            SelectionItemPattern select = (SelectionItemPattern)listItem.GetCurrentPattern(SelectionItemPattern.Pattern);
            //SelectionItemPattern select = (SelectionItemPattern)currentElement.GetCurrentPattern(SelectionItemPattern.Pattern);
            select.Select();
            //text.SetValue(valueToSelect);
        }
    }
}
