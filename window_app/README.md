# logger-restclient
The purpose of 

## 1.0 Required Software
- .NET Core 3.1       (Used to develop backend API, WPF frontend application and API unit test scripts)
- .NET Framework 4.5  (Used to develop GUI test scipts using UI Automation)
- VS 2019             (Only required to debug the application)

## 2.0 Build the Project
The steps to build the projects are as follows:
- Open CMD
- git clone https://github.com/umairnaushad/parser-restclient.git
- cd parser-restclient\window_app
- build_application.bat
<br/><br/>
Two projects will be build and output should be similar to 
![alt text](https://github.com/umairnaushad/parser-restclient/blob/main/snapshots/Build-Application.png)
<br/><br/><br/>

## 3.0 Execute Desktop Application
The steps to execute the application are as follows:
- cd exe
- RijksMuseumApplication.exe

### 3.1 Using Desktop Application
After launching the application, below window will appear
![alt text](https://github.com/umairnaushad/parser-restclient/blob/main/snapshots/Launch-App.png)
<br /><br /><br />

Select artist from the combo box and then click on "Fetch Collection List" butotn
![alt text](https://github.com/umairnaushad/parser-restclient/blob/main/snapshots/Fetch-Data.png)
<br /><br /><br />


Click on a picture to get detail view of a collection
![alt text](https://github.com/umairnaushad/parser-restclient/blob/main/snapshots/Collection-Detail.png)

## 4.0 Execute Automation Test Cases/Scripts
### 4.1 Build the Test Projects
The solution contains two automation testing projects. Once for the unit test cases for backend API and second one for the UI Automation test cases for the GUI.
To build the test projects make sure that you are in "parser-restclient\window_app" folder and eexecute below command on CMD:
- build_test_projects.bat
<br/><br/>
Two projects will be build and output should be similar to 
![alt text](https://github.com/umairnaushad/parser-restclient/blob/main/snapshots/Build-AutomationTests.png)
<br/><br/><br/>
### 4.2 Execute the Unit Test Cases
The steps to execute the application are as follows:

### 4.2 Execute the GUI Test Cases
Make sure that you are in "parser-restclient\window_app\exe" folder and execute the below command:


dotnet test RESTClientNUnitTests.dll --logger "console;verbosity=detailed;html"
dotnet test --logger "console;verbosity=detailed;html"
&nbsp;
&nbsp;


&nbsp;
dotnet test RESTClientNUnitTests.dll --logger html

dotnet test window_app\source\RESTClientNUnitTests\ --logger html

report path:
\window_app\source\RESTClientNUnitTests\TestResults
