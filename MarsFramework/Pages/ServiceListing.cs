using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsFramework.Global;
using NUnit.Framework;

namespace MarsFramework.Pages
{
    internal class ServiceListing
    {
        private IList<string> list;
        public ServiceListing(IList<string> list)
        {
            PageFactory.InitElements(GlobalDefinitions.driver, this);
            this.list = list;
        }

        #region  Initialize Web Elements

        [FindsBy(How = How.XPath, Using = "//div[@class='ui grid service-details']/div[@class='row'][2]/div[1]/descendant::span[@class='skill-title']")]
        private IWebElement skillTitle;
       
        [FindsBy(How = How.XPath, Using = "//div[@class='ui grid service-details']/div[@class='row'][2]/div[1]/descendant::div[@class='description'][1]")]
        private IWebElement skillDescription;
       
        [FindsBy(How = How.XPath, Using = "//div[@class='ui grid service-details']/div[@class='row'][2]/div[1]/descendant::div[@class='description'][2]")]
        private IWebElement skillCategory;

        [FindsBy(How = How.XPath, Using = "//div[@class='ui grid service-details']/div[@class='row'][2]/div[1]/descendant::div[@class='description'][3]")]
        private IWebElement skillSubCategory;

        [FindsBy(How = How.XPath, Using = "//div[@class='ui grid service-details']/div[@class='row'][2]/div[1]/descendant::div[@class='description'][4]")]
        private IWebElement ServiceType;

        [FindsBy(How = How.XPath, Using = "//div[@class='ui grid service-details']/div[@class='row'][2]/div[1]/descendant::div[@class='description'][5]")]
        private IWebElement startDate;

        [FindsBy(How = How.XPath, Using = "//div[@class='ui grid service-details']/div[@class='row'][2]/div[1]/descendant::div[@class='description'][6]")]
        private IWebElement endDate;

        [FindsBy(How = How.XPath, Using = "//div[@class='ui grid service-details']/div[@class='row'][2]/div[1]/descendant::div[@class='description'][7]")]
        private IWebElement locationType;
        
        [FindsBy(How = How.XPath, Using = "//div[@class='ui grid service-details']/div[@class='row'][2]/div[2]/descendant::div[@class='user-info']/a/h3")]
        private IWebElement userName;

        #endregion

        // Check if user can see the same service on which he/she clicked for further details
        internal void isServiceDetailsCorrect()
        {
            GlobalDefinitions.wait(10);
            Assert.Multiple(() =>
            {
                Assert.That(skillTitle.Text, Is.EqualTo(this.list[0]));
                Assert.That(skillDescription.Text, Is.EqualTo(this.list[2]));
                Assert.That(skillCategory.Text, Is.EqualTo(this.list[1])); 
                Assert.That(ServiceType.Text, Is.EqualTo(this.list[3]));
            });
        }

    }
}
