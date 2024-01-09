
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using SpecFlowProject2.Drivers;
using SpecFlowProject2.Pages;
using System;
using System.Threading;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowProject2.Steps
{
    [Binding]
    public class Google_RemoteDriverSteps
    {
        private IWebDriver driver;

        private readonly ScenarioContext _scenarioContext;

        
        public Google_RemoteDriverSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I am on the remote google page")]
        public void GivenIAmOnTheRemoteGooglePage(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            driver = _scenarioContext.Get<SeleniumDriver>("SeleniumDriver").SetupRemmote((string)data.Browsername);
            driver.Url = "https://www.google.com";
        }
        
        [When(@"I search for ""(.*)"" on remote")]
        public void WhenISearchForOnRemote(string p0)
        {
            Page.GoHome.EnterDataToSearch(p0);
        }
        
        [Then(@"I should see title ""(.*)"" on remote")]
        public void ThenIShouldSeeTitleOnRemote(string p0)
        {
            Thread.Sleep(5000);
            Assert.That(driver.Title, Is.EqualTo(p0));
        }
    }
}
