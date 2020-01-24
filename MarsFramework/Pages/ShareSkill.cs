using OpenQA.Selenium;
using MarsFramework.Global;
using OpenQA.Selenium.Support.PageObjects;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using MarsFramework.Config;
using MarsFramework.Custom_Methods;

namespace MarsFramework.Pages
{
    internal class ShareSkill
    {
        public ShareSkill()
        {
            PageFactory.InitElements(GlobalDefinitions.driver, this);
        }

        #region Service Listing Page Elements 
        //Enter the Title in textbox
        [FindsBy(How = How.Name, Using = "title")]
        private IWebElement Title { get; set; }

        //Enter the Description in textbox
        [FindsBy(How = How.Name, Using = "description")]
        private IWebElement Description { get; set; }

        //Click on Category Dropdown
        [FindsBy(How = How.Name, Using = "categoryId")]
        private IWebElement CategoryDropDown { get; set; }

        //Click on SubCategory Dropdown
        [FindsBy(How = How.Name, Using = "subcategoryId")]
        private IWebElement SubCategoryDropDown { get; set; }

        //Enter Tag names in textbox
        [FindsBy(How = How.XPath, Using = "//body/div/div/div[@id='service-listing-section']/div[contains(@class,'ui container')]/div[contains(@class,'listing')]/form[contains(@class,'ui form')]/div[contains(@class,'tooltip-target ui grid')]/div[contains(@class,'twelve wide column')]/div[contains(@class,'')]/div[contains(@class,'ReactTags__tags')]/div[contains(@class,'ReactTags__selected')]/div[contains(@class,'ReactTags__tagInput')]/input[1]")]
        private IWebElement Tags { get; set; }

        //Select the Service type
        [FindsBy(How = How.XPath, Using = "//form/div[5]/div[@class='twelve wide column']/div/div[@class='field']")]
        private IWebElement ServiceTypeOptions { get; set; }

        //Select the Location Type
        [FindsBy(How = How.XPath, Using = "//form/div[6]/div[@class='twelve wide column']/div/div[@class = 'field']")]
        private IWebElement LocationTypeOptions { get; set; }

        //Click on Start Date dropdown
        [FindsBy(How = How.Name, Using = "startDate")]
        private IWebElement StartDateDropDown { get; set; }

        //Click on End Date dropdown
        [FindsBy(How = How.Name, Using = "endDate")]
        private IWebElement EndDateDropDown { get; set; }

        //Storing the table of available days
        [FindsBy(How = How.XPath, Using = "//body/div/div/div[@id='service-listing-section']/div[@class='ui container']/div[@class='listing']/form[@class='ui form']/div[7]/div[2]/div[1]")]
        private IWebElement Days { get; set; }

        //Storing the starttime
        [FindsBy(How = How.XPath, Using = "//div[3]/div[2]/input[1]")]
        private IWebElement StartTime { get; set; }

        //Click on StartTime dropdown
        [FindsBy(How = How.XPath, Using = "//div[3]/div[2]/input[1]")]
        private IWebElement StartTimeDropDown { get; set; }

        //Click on EndTime dropdown
        [FindsBy(How = How.XPath, Using = "//div[3]/div[3]/input[1]")]
        private IWebElement EndTimeDropDown { get; set; }

        //Click on Skill Trade option
        [FindsBy(How = How.XPath, Using = "//form/div[8]/div[@class='twelve wide column']/div/div[@class = 'field']")]
        private IWebElement SkillTradeOption { get; set; }

        //Enter Skill Exchange
        [FindsBy(How = How.XPath, Using = "//div[@class='form-wrapper']//input[@placeholder='Add new tag']")]
        private IWebElement SkillExchange { get; set; }

        //Enter the amount for Credit
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Amount']")]
        private IWebElement CreditAmount { get; set; }

        //Click on Active/Hidden option
        [FindsBy(How = How.XPath, Using = "//form/div[10]/div[@class='twelve wide column']/div/div[@class = 'field']")]
        private IWebElement ActiveOption { get; set; }

        //Click on upload button
        [FindsBy(How = How.XPath, Using = "//i[@class='huge plus circle icon padding-25']")]
        private IWebElement fileUpload;

        //Click on Save button
        [FindsBy(How = How.XPath, Using = "//input[@value='Save']")]
        private IWebElement Save { get; set; }
        #endregion

        internal void EnterShareSkill()
        {
            //Pouplate ShareSkill Data into ExcelReader
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPath, "ShareSkill");

            //Enter the data on HTML form
            Title.EnterText(GlobalDefinitions.ExcelLib.ReadData(2,"Title"));
            Description.EnterText(GlobalDefinitions.ExcelLib.ReadData(2,"Description"));

            //Select Category and Subcategory
            CategoryDropDown.selectDropDown(GlobalDefinitions.ExcelLib.ReadData(2, "Category"));
            SubCategoryDropDown.selectDropDown(GlobalDefinitions.ExcelLib.ReadData(2, "SubCategory"));

            //Enter tag
            Tags.EnterText(GlobalDefinitions.ExcelLib.ReadData(2, "Tags"));
            Tags.EnterText(Keys.Enter);

            //Select Radio button
            selectServiceRadioButton(ServiceTypeOptions, GlobalDefinitions.ExcelLib.ReadData(2, "ServiceType"));

            selectLocationRadioButton(LocationTypeOptions, GlobalDefinitions.ExcelLib.ReadData(2, "LocationType"));

            // select start and end data and time
            //selectStartDateAndTime();

            // select skill trade: Skill exchange or credit
            selectSkillTrade(SkillTradeOption, GlobalDefinitions.ExcelLib.ReadData(2, "Credit"));

            // Upload work sample.docx
            //fileUpload.Click();
            //GlobalDefinitions.FileUpload.uploadFileUsingAutoIT(@MarsResource.FileUpload);

            GlobalDefinitions.wait(5);

            // select Active Radio button: Active or Hidden
            selectActiveRadioButton(ActiveOption, GlobalDefinitions.ExcelLib.ReadData(2, "Active"));


        }
        internal ManageListings clickOnSaveBtn()
        {
            // Click on Save button
            Save.Clicks();
            return new ManageListings();
        }
        // Update the description
        internal string EditShareSkill()
        {
            Description.Clear();
            string str = "2 hours session for Selenium!";
            Description.EnterText(str);
            return str;
        }

        // Select Radio button
        private void selectServiceRadioButton(IWebElement element, string type)
        {
            if (type == "One-off service")
            {
                element.FindElement(By.XPath("//div[@class='ui radio checkbox']/input[@value= '1' and @name='serviceType']")).Click();
            }
            else
            {
                element.FindElement(By.XPath("//div[@class='ui radio checkbox']/input[@value = '0' and @name='serviceType']")).Click();
            }
        }

        private void selectLocationRadioButton(IWebElement element, string type)
        {
            if (type == "On-site")
            {
                element.FindElement(By.XPath("//div[@class='ui radio checkbox']/input[@value = '0' and @name='locationType']")).Click();
            }
            else
            {
                element.FindElement(By.XPath("//div[@class='ui radio checkbox']/input[@value = '1' and @name = 'locationType']")).Click();
            }
        }

        private void selectStartDateAndTime()
        {
            string startDate = GlobalDefinitions.ExcelLib.ReadData(2, "Startdate").Replace('-',' ').Trim(' ');
            Console.WriteLine("Date Time: "+ startDate + "Reading directly from Excel "+ GlobalDefinitions.ExcelLib.ReadData(2, "Startdate"));
            StartDateDropDown.enterDateAndTimeWithTab(GlobalDefinitions.ExcelLib.ReadData(2, "Startdate"), '-');
            
            EndDateDropDown.enterDateAndTimeWithTab(GlobalDefinitions.ExcelLib.ReadData(2, "Enddate"), '-');
            

            string weekDay = GlobalDefinitions.ExcelLib.ReadData(2, "Selectday");

            switch (weekDay)
            {   //MOnday is default value
                case "Sun":
                    Days.FindElement(By.XPath("//div[2]/descendant::input[@index='0' and @name='Available']")).Click();
                    Days.FindElement(By.XPath("//div[2]/descendant::input[@index='0' and @name='StartTime']")).
                        enterDateAndTimeWithTab(GlobalDefinitions.ExcelLib.ReadData(2,"Starttime"), ' ');
                    Days.FindElement(By.XPath("//div[2]/descendant::input[@index='0' and @name='EndTime']")).
                        enterDateAndTimeWithTab(GlobalDefinitions.ExcelLib.ReadData(2, "Endtime"), ' ');
                    break;
                case "Tue":
                    Days.FindElement(By.XPath("//div[4]/descendant::input[@index='2' and @name='Available']")).Click();
                    Days.FindElement(By.XPath("//div[4]/descendant::input[@index='2' and @name='StartTime']")).
                        enterDateAndTimeWithTab(GlobalDefinitions.ExcelLib.ReadData(2, "Starttime"), ' ');
                    Days.FindElement(By.XPath("//div[4]/descendant::input[@index='2' and @name='EndTime']")).
                        enterDateAndTimeWithTab(GlobalDefinitions.ExcelLib.ReadData(2, "Endtime"), ' ');
                    break;
                case "Wed":
                    Days.FindElement(By.XPath("//div[5]/descendant::input[@index='3' and @name='Available']")).Click();
                    Days.FindElement(By.XPath("//div[5]/descendant::input[@index='3' and @name='StartTime']")).
                       enterDateAndTimeWithTab(GlobalDefinitions.ExcelLib.ReadData(2, "Starttime"), ' ');
                    Days.FindElement(By.XPath("//div[5]/descendant::input[@index='3' and @name='EndTime']")).
                        enterDateAndTimeWithTab(GlobalDefinitions.ExcelLib.ReadData(2, "Endtime"), ' ');
                    break;
                case "Thu":
                    Days.FindElement(By.XPath("//div[6]/descendant::input[@index='4' and @name='Available']")).Click();
                    Days.FindElement(By.XPath("//div[6]/descendant::input[@index='4' and @name='StartTime']")).
                        enterDateAndTimeWithTab(GlobalDefinitions.ExcelLib.ReadData(2, "Starttime"), ' ');
                    Days.FindElement(By.XPath("//div[6]/descendant::input[@index='4' and @name='EndTime']")).
                        enterDateAndTimeWithTab(GlobalDefinitions.ExcelLib.ReadData(2, "Endtime"), ' ');
                    break;
                case "Fri":
                    Days.FindElement(By.XPath("//div[7]/descendant::input[@index='5' and @name='Available']")).Click();
                    Days.FindElement(By.XPath("//div[7]/descendant::input[@index='5' and @name='StartTime']")).
                        enterDateAndTimeWithTab(GlobalDefinitions.ExcelLib.ReadData(2, "Starttime"), ' ');
                    Days.FindElement(By.XPath("//div[7]/descendant::input[@index='5' and @name='EndTime']")).
                        enterDateAndTimeWithTab(GlobalDefinitions.ExcelLib.ReadData(2, "Endtime"), ' ');
                    break;
                case "Sat":
                    Days.FindElement(By.XPath("//div[8]/descendant::input[@index='6' and @name='Available']")).Click();
                    Days.FindElement(By.XPath("//div[8]/descendant::input[@index='6' and @name='StartTime']")).
                        enterDateAndTimeWithTab(GlobalDefinitions.ExcelLib.ReadData(2, "Starttime"), ' ');
                    Days.FindElement(By.XPath("//div[8]/descendant::input[@index='6' and @name='EndTime']")).
                        enterDateAndTimeWithTab(GlobalDefinitions.ExcelLib.ReadData(2, "Endtime"), ' ');
                    break;
                default:
                    Days.FindElement(By.XPath("//div[3]/descendant::input[@index='1' and @name='Available']")).Click();
                    Days.FindElement(By.XPath("//div[3]/descendant::input[@index='1' and @name='StartTime']")).
                        enterDateAndTimeWithTab(GlobalDefinitions.ExcelLib.ReadData(2, "Starttime"), ' ');
                    Days.FindElement(By.XPath("//div[3]/descendant::input[@index='1' and @name='EndTime']")).
                        enterDateAndTimeWithTab(GlobalDefinitions.ExcelLib.ReadData(2, "Endtime"),' ');
                    break;
            }

        }

        private void selectSkillTrade(IWebElement element, string skillTrade)
        {
            if (skillTrade == "Skill-Exchange")
            {
                element.FindElement(By.XPath("//div[@class='ui radio checkbox']/input[@value = 'true' and @name='skillTrades']")).Click();
                if(SkillExchange.Displayed && SkillExchange.Enabled)
                {
                    SkillExchange.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Skill-Exchange"));
                    SkillExchange.SendKeys(Keys.Enter);
                }
                else
                {
                    Console.WriteLine("Skill Exchange textbox is not avaiable");
                    return;
                }
            }
            else if(skillTrade == "Credit")
            {
                IWebElement credit = element.FindElement(By.XPath("//div[@class='ui radio checkbox']/input[@value = 'false' and @name = 'skillTrades']"));
                credit.Click();
                GlobalDefinitions.wait(5);
                if (CreditAmount.Displayed && CreditAmount.Enabled)
                {
                    CreditAmount.SendKeys("1");
                }
                else
                {
                    Console.WriteLine("Credit textbox is not avaiable");
                    return;
                }
            }
        }

        private void selectActiveRadioButton(IWebElement element, string active)
        {
            if (active == "Active")
            {
                element.FindElement(By.XPath("//div[@class='ui radio checkbox']/input[@value = 'true' and @name='isActive']")).Click();
               
            }
            else if (active == "Hidden")
            {
                element.FindElement(By.XPath("//div[@class='ui radio checkbox']/input[@value = 'false' and @name ='isActive']")).Click();    
            }
        }

       

        
    }
}
