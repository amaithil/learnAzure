using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;

namespace SpecFlowProject2.Pages
{
   public class Homepage
    {
        private IWebDriver webDriver;
        [FindsBy(How = How.Name, Using = "li1")]
        [CacheLookup]
        private IWebElement Item1 { get; set; }
        [FindsBy(How = How.Name, Using = "li2")]
        public IWebElement Item2 { get; set; }
        [FindsBy(How = How.Id, Using = "sampletodotext")]
        public IWebElement Item3 { get; set; }
        [FindsBy(How = How.Id, Using = "addbutton")]
        public IWebElement Item4 { get; set; }
        [FindsBy(How = How.XPath, Using = " / html / body / div / div / div / form / input[1]")]
        public IWebElement Item5 { get; set; }

        public void ClickCheckOut1Button()
        {
            Console.WriteLine("test from Aditya ****####");
            Item1.Click();
        }
        public void ClickCheckOut2Button()
        {
            Item2.Click();
        }
        public void ClickCheckOut3Button()
        {
            Item4.Click();
        }
    }
}
