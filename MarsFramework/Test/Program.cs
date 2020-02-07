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
                GlobalDefinitions.wait(40);
                string currentUrl = GlobalDefinitions.driver.Url;
                Console.WriteLine(currentUrl);
                Assert.That(currentUrl, Is.EqualTo("http://192.168.99.100:5000/Account/Profile"));
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
                GlobalDefinitions.wait(20);
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