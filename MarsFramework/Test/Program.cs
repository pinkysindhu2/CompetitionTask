using NUnit.Framework;
using MarsFramework.Global;
using MarsFramework.Pages;
using System;
using System.Threading;
using MarsFramework.Config;


namespace MarsFramework
{
    public class Program
    {
        [TestFixture]
        [Category("Sprint1")]
        class User : Base
        {

            [Test, Order(1)]
            public void LoginSuccess()
            {
                Thread.Sleep(3000);
                string currentUrl = GlobalDefinitions.driver.Url;
                Console.WriteLine(currentUrl);
                Assert.That(currentUrl, Is.EqualTo("http://www.skillswap.pro/Account/Profile"));
            }

            [Test, Order(2)]
            public void EnterShareSkill()
            {
                Thread.Sleep(3000);
                NavigationBar navigationBar = new NavigationBar();
                ShareSkill shareSkill = navigationBar.clickOnShareSkilBtn();
                shareSkill.EnterShareSkill();
                GlobalDefinitions.wait(20);
                ManageListings manageListings = shareSkill.clickOnSaveBtn();
                manageListings.isServiceSaved();
            }

            [Test, Order(3)]
            public void viewListedService()
            {
                NavigationBar navigationBar = new NavigationBar();
                ManageListings manageListings = navigationBar.clickOnManageListing();
                ServiceListing serviceListing = manageListings.viewListedService();
                serviceListing.isServiceDetailsCorrect();
            }

            [Test, Order(4)]
            public void EditShareSkill()
            {
                NavigationBar navigationBar = new NavigationBar();
                ManageListings manageListings = navigationBar.clickOnManageListing();
                ShareSkill shareSkill = manageListings.clickOnEdit();
                string description = shareSkill.EditShareSkill();
                ManageListings manageListing = shareSkill.clickOnSaveBtn();
                manageListing.isServiceUpdated(description);
            }

            [Test, Order(5)]
            public void DeleteListedService()
            {
                NavigationBar navigationBar = new NavigationBar();
                ManageListings manageListings = navigationBar.clickOnManageListing();
                manageListings.deleteListedService();
            }

        }
    }
}