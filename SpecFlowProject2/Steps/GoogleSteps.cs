using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using SpecFlowProject2.Drivers;
using SpecFlowProject2.Pages;
using System;
using System.Threading;
using TechTalk.SpecFlow;

namespace SpecFlowProject2.Features
{
    [Binding]
    public class GoogleSteps
    {

        private IWebDriver driver;

        private readonly ScenarioContext _scenarioContext;

        public GoogleSteps(ScenarioContext scenarioContext) {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I am on the google page")]
        public void GivenIAmOnTheGooglePage()
        {
            driver =_scenarioContext.Get<SeleniumDriver>("SeleniumDriver").Setup();
            driver.Url = "https://www.google.com";
        }

        [When(@"I search for ""(.*)""")]
        public void WhenISearchFor(string p0)
        {
            Page.GoHome.EnterDataToSearch(p0);
        }

        [Then(@"I should see title ""(.*)""")]
        public void ThenIShouldSeeTitle(string p0)
        {
            Thread.Sleep(5000);
            Assert.AreEqual(p0,driver.Title);
            
        }

    }
}
