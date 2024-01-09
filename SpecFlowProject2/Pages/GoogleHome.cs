using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SpecFlowProject2.Hooks;
using System;
using System.Text;
using TechTalk.SpecFlow;

namespace SpecFlowProject2.Pages
{
    public class GoogleHome
    {
        private readonly IWebDriver webDriver;
  

        [FindsBy(How = How.XPath, Using = "//*[@name='q']")]
        private IWebElement googleSearchBar { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@name='b']")]
        private IWebElement iMFeelingLucky { get; set; }



        public void EnterDataToSearch(String input)
        {
            googleSearchBar.SendKeys(input);
            googleSearchBar.SendKeys(Keys.Enter);
        }

    }
}
