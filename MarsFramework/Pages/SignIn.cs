using System;
using OpenQA.Selenium;
using MarsFramework.Global;
using SeleniumExtras.PageObjects;


namespace MarsFramework.Pages
{
    class SignIn
    {

        public SignIn()
        {
            PageFactory.InitElements(this, new RetryingElementLocator(GlobalDefinitions.driver, TimeSpan.FromSeconds(20)));
        }

        #region  Initialize Web Elements 
        //Finding the Sign Link
        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Sign')]")]
        private IWebElement SignIntab { get; set; }

        // Finding the Email Field
        [FindsBy(How = How.Name, Using = "email")]
        private IWebElement Email { get; set; }

        //Finding the Password Field
        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement Password { get; set; }

        //Finding the Login Button
        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Login')]")]
        private IWebElement LoginBtn { get; set; }

        #endregion

        internal void LoginSteps()
        {
            // Naviagte to the Mars project
            GlobalDefinitions.driver.Navigate().GoToUrl(GlobalDefinitions.projectUrl);
            GlobalDefinitions.wait(2000);
            SignIntab.Click();

            // Enter the username and password
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPath, "Login");
            GlobalDefinitions.wait(1000);
            Console.WriteLine(GlobalDefinitions.ExcelLib.ReadData(2, "Email"));
            Email.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2,"Email"));
            Password.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Password"));

            LoginBtn.Click();
            //GlobalDefinitions.wait(40);
        }


    }
}