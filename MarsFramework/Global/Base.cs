using MarsFramework.Config;
using MarsFramework.Pages;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using RelevantCodes.ExtentReports;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using static MarsFramework.Global.GlobalDefinitions;

namespace MarsFramework.Global
{
    class Base
    {
        #region To access Path from resource file

        public static int Browser = Int32.Parse(MarsResource.Browser);
        // Get the current directory MarsFramework
        public static String ExcelPath =  @MarsResource.ExcelPath;
        public static string ScreenshotPath = @MarsResource.ScreenShotPath;
        public static string ReportPath = @MarsResource.ReportPath;
        public static string ReportXmlPath = @MarsResource.ReportXMLPath;
        /*private static string solutionParentDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
        public static String ExcelPath = Path.Combine(solutionParentDirectory, @MarsResource.ExcelPath);
        public static string ScreenshotPath = Path.Combine(solutionParentDirectory, @MarsResource.ScreenShotPath);
        public static string ReportPath = Path.Combine(solutionParentDirectory, @MarsResource.ReportPath);
        public static string ReportXmlPath = Path.Combine(solutionParentDirectory, @MarsResource.ReportXMLPath);*/

        #endregion

        #region reports
        public static ExtentTest test;
        public static ExtentReports extent;
        #endregion

        #region setup and tear down
        [OneTimeSetUp]
        public void Inititalize()
        {
            Process.Start(@"C:\Users\Pinky Sindhu\Desktop\Industry Connect\Industry Connect\Internship\Task 2\marsframework-master\MarsFramework\startDockerEngine.bat");
            Thread.Sleep(120000);
            // run the docker-compose file
            RunDockerComposeFile();
            Thread.Sleep(90000);
            // advisasble to read this documentation before proceeding http://extentreports.relevantcodes.com/net/
            switch (Browser)
            {

                case 1:
                    GlobalDefinitions.driver = new FirefoxDriver();
                    break;
                case 2:
                    Thread.Sleep(3000);
                    GlobalDefinitions.driver = new ChromeDriver();
                    GlobalDefinitions.driver.Manage().Window.Maximize();
                    GlobalDefinitions.driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(40);
                    break;

            }

            #region Initialise Reports

            extent = new ExtentReports(ReportPath, false, DisplayOrder.NewestFirst);
            extent.LoadConfig(ReportXmlPath);
            test = extent.StartTest("Project Mars");

            #endregion

            if (MarsResource.IsLogin == "true")
            {
                SignIn loginobj = new SignIn();
                loginobj.LoginSteps();
            }
            else
            {
                SignUp obj = new SignUp();
                obj.register();
            }

        }


        [OneTimeTearDown]
        public void TearDown()
        {
            // Screenshot
            String img = SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "Report");//AddScreenCapture(@"E:\Dropbox\VisualStudio\Projects\Beehive\TestReports\ScreenShots\");
            test.Log(LogStatus.Info, "Image example: " + img);
            // end test. (Reports)
            extent.EndTest(test);
            // calling Flush writes everything to the log file (Reports)
            extent.Flush();
            // Close the driver :)            
            GlobalDefinitions.driver.Close();
            GlobalDefinitions.driver.Quit();
            StopDockerComposeFile();
            Process.Start(@"C:\Users\Pinky Sindhu\Desktop\Industry Connect\Industry Connect\Internship\Task 2\marsframework-master\MarsFramework\stopDockerEngine.bat");
            EndProcess();

        }
        #endregion

        #region Run Docker and Stop
        private void RunDockerComposeFile()
        {
            try
            {
               
                string FileName = @"C:\Users\Pinky Sindhu\Desktop\Industry Connect\Industry Connect\Internship\Task 2\marsframework-master\MarsFramework\startPMWebsite.bat";
                
                Process.Start(FileName);
                  
            }
            catch(Exception e)
            {
                Console.WriteLine("Error in bat file {0} with the following {1} execption ", "startPMWebsite.bat", e);
            }
            
        }

        private void StopDockerComposeFile()
        {
            try
            {
                string FileName = @"C:\Users\Pinky Sindhu\Desktop\Industry Connect\Industry Connect\Internship\Task 2\marsframework-master\MarsFramework\stopPMWebsite.bat";

                Process.Start(FileName);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in bat file {0} with the following {1} execption ", "startPMWebsite.bat", e);
            }
        }
        private void EndProcess()
        {
            Process[] process = Process.GetProcesses();
            foreach (var p in process)
            {
                Console.WriteLine("Process Nanme " + p.ProcessName);
                p.Kill();
            }
        }
        #endregion
    }
}