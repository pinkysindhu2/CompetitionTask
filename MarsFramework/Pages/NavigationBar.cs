using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using MarsFramework.Global;
using MarsFramework.Custom_Methods;
using OpenQA.Selenium.Support.Extensions;

namespace MarsFramework.Pages
{
    public class NavigationBar
    {
        public  NavigationBar()
        {
            PageFactory.InitElements(GlobalDefinitions.driver, this) ;
        }

        // Initialize the Naviagtion bar tab which are common on pages
        #region Common Tab web elements 

        [FindsBy(How = How.LinkText, Using = "Share Skill")]
        private IWebElement shareSkillBtn { get; set; }

        [FindsBy(How = How.LinkText, Using = "Profile")]
        private IWebElement profileTab { get; set; }

        [FindsBy(How = How.LinkText, Using = "Manage Listings")]
        private IWebElement manageListingTab { get; set; }

        [FindsBy(How = How.LinkText, Using = "Dashboard")]
        private IWebElement dashTab { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[text()= 'Manage Requests']")]
        private IWebElement manageRequestDDL;
        #endregion

        internal ShareSkill clickOnShareSkilBtn()
        {
            shareSkillBtn.Clicks();
            return new ShareSkill();
        }

        internal ManageListings clickOnManageListing()
        {
            manageListingTab.Clicks();
            return new ManageListings();
        }
    
    }
}
