using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsFramework.Custom_Methods
{
   public static class SeleniumSetMethods
   {
        public static void EnterText( this IWebElement element, string value)
        {
            element.SendKeys(value);
        }

        public static void selectDropDown(this IWebElement element, string text)
        {
            new SelectElement(element).SelectByText(text);
        }

        public static void Clicks(this IWebElement element)
        {
            element.Click();
        }
        public static void enterDateAndTimeWithTab(this IWebElement element, string value, char splitChar)
        {
            if (value != null)
            {
                IList<string> txt = value.Split(splitChar);
                for (int i = 0; i < txt.Count; i++)
                {
                    
                    element.EnterText(txt[i]);
                    //Global.GlobalDefinitions.wait(1);
                    Console.WriteLine(txt[i]);
                    if (i < txt.Count - 1)
                    {
                        element.EnterText(Keys.Tab);
                    }

                }
            }
            else
            {
                Console.WriteLine("{0} is null " + value);
            }
        }
    }
}
