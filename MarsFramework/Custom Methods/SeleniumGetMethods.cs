using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsFramework.Custom_Methods
{
   public static class SeleniumGetMethods
   {
        public static string GetText(this IWebElement element)
        {
            return element.GetAttribute("value");
        }

        public static string GetTextFromDDL(this IWebElement element)
        {  
            return new SelectElement(element).AllSelectedOptions.SingleOrDefault().Text;
        }
    }
}
