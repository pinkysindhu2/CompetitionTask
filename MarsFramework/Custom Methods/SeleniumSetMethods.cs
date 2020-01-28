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
        public static void setDate(this IWebElement element, string value)
        {
            /* if (value != null)
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
             }*/
            Console.WriteLine("Get the Date from Date Picker: " + element.GetAttribute("value"));
            //string dateFormat = "dd/MM/yyyy";
            string dateFormat = "ddMMyyyy";
            string newDate =  DateTime.Parse(value).ToString(dateFormat);
            
            element.EnterText(newDate);
           // Actions a = new Actions(driver);
           // a.keyDown(Keys.TAB).perform();
            Global.GlobalDefinitions.wait(30);

            Console.WriteLine("Date Formated: "+newDate);
            
        }

        public static void setTime(this IWebElement element, string value)
        {
            string timeFormat = "hh:mmtt";
            string newTime = DateTime.Parse(value).ToString(timeFormat);
            element.EnterText(newTime);
            Console.WriteLine("Time Formated: "+ newTime);
            Global.GlobalDefinitions.wait(10);
        }
    }
}
