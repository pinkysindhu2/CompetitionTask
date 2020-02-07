using MarsFramework.Global;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using MarsFramework.Custom_Methods;

namespace MarsFramework.Pages
{
    internal class ManageListings
    {
        private string url = "http://192.168.99.100:5000/Home/ListingManagement";
        public ManageListings()
        {
            PageFactory.InitElements(GlobalDefinitions.driver, this);
        }
        #region Initlize web elements
        //Get the user name
        [FindsBy(How = How.XPath, Using = "//span[@class='item ui dropdown link']")]
        private IWebElement userName;

        //Click on Manage Listings Link
        [FindsBy(How = How.LinkText, Using = "Manage Listings")]
        private IWebElement manageListingsLink { get; set; }

        // table element
        [FindsBy(How = How.TagName, Using = "table" )]
        private IWebElement table { get; set; }
        //View the listing
        [FindsBy(How = How.XPath, Using = "(//i[@class='eye icon'])[1]")]
        private IWebElement view { get; set; }

        //Delete the listing
        [FindsBy(How = How.XPath, Using = "(//i[@class='remove icon'])[1]")]
        private IWebElement delete { get; set; }

        //Edit the listing
        [FindsBy(How = How.XPath, Using = "(//i[@class='outline write icon'])[1]")]
        private IWebElement edit { get; set; }

        //Click on Yes or No
        [FindsBy(How = How.XPath, Using = "//div[@class='actions']")]
        private IWebElement clickActionsButton { get; set; }

        #endregion

        // newly created service is listed or not
        internal void isServiceSaved()
        {
            // find the table element is displayed or enabled which means that 1 and more Services are there
            if(verifyListingAvabilability())
            {
                // check the Service is listed or not: Saved or updated
                GlobalDefinitions.wait(30);
                int currentRows = getTotalRows();
                isServiceListed(currentRows-1, currentRows, "Save");
            }
            else
            {
                Console.WriteLine("Table element is not present.");
            }
        }

        internal ShareSkill clickOnEdit()
        {
            edit.Clicks();
            return new ShareSkill();
  
        }

        internal ServiceListing viewListedService()
        {
            GlobalDefinitions.wait(10);
            IList<String> list = new List<String>();
            if (verifyListingAvabilability())
            {
                
                var title = table.FindElement(By.XPath("//table/tbody/tr[1]/td[3]")).Text;
                list.Add(title);
                var category = table.FindElement(By.XPath("//table/tbody/tr[1]/td[2]")).Text;
                list.Add(category);
                var description = table.FindElement(By.XPath("//table/tbody/tr[1]/td[4]")).Text;
                list.Add(description);
                var serviceType = table.FindElement(By.XPath("//table/tbody/tr[1]/td[5]")).Text;
                list.Add(serviceType);

                view.Click();
                
            }
            if(list.Count != 0)
            {
                return new ServiceListing(list);
            }
            else
            {
                return null;
            }
        }

        //Check the edit is success or not
        internal void isServiceUpdated(string description)
        {
            GlobalDefinitions.wait(10);
            if (verifyListingAvabilability())
            {
                IWebElement desc = table.FindElement(By.XPath("//table/tbody/tr[1]/td[4]"));
                Assert.That(desc.Text, Is.EqualTo(description));
            }

        }

        // Click on Manage Listing Tab
        private void clickOnManageListing()
        {
            manageListingsLink.Click();
        }



        // Checking the Deleted  Service is not listed
        private void isServiceListed(int totalRows, int currentRows, string methodName)
        {
            Console.WriteLine("Total Rows: " + totalRows + " Current Rows: "+currentRows);
            if( methodName == "Delete")
            {
                GlobalDefinitions.wait(30);
                Assert.That(currentRows, Is.LessThan(totalRows));
            }
            else if( methodName == "Save")
            {
                Assert.Multiple(() => {
                    GlobalDefinitions.wait(30);
                    Assert.That(GlobalDefinitions.driver.Url, Is.EqualTo(url));
                    Assert.That(currentRows, Is.GreaterThan(totalRows));
                });
                
            }

        }

        // Delete a service with name of Selenium
        internal void deleteListedService()
        {
            //click on Manage Listimg tab
            clickOnManageListing();
            // Click on Delete button
            if (verifyListingAvabilability())
            {
                // Before Delete option
                int totalRow = getTotalRows();
                
                delete.Click();
                
                if (clickActionsButton.Displayed && clickActionsButton.Enabled)
                {
                    clickActionsButton.FindElement(By.XPath("//div[@class='actions']/button[2]")).Click();
                    GlobalDefinitions.wait(15);
                    if (verifyListingAvabilability())
                    {
                        
                        int currentRows = getTotalRows();
                        isServiceListed(totalRow,currentRows, "Delete");

                    }
                }
                else
                {
                    Assert.Fail("Unable to delete");
                }
            }
            else
            {
                Assert.Warn("No Service is listed for delete!");
            }
            
            
           
        }

        // check the Selenium named Service is available to perform Actions: View, Edit and delete
        private bool verifyListingAvabilability()
        {
            return table.Displayed && table.Enabled;
        }

        private int getTotalRows()
        {
            GlobalDefinitions.wait(20);
            IWebElement tbody = table.FindElement(By.XPath("//tbody"));
            IList<IWebElement> rows = tbody.FindElements(By.XPath("//tbody/tr"));
            return rows.Count;
        }
    }
}
