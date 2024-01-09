using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using TechTalk.SpecFlow;
using WebDriverManager.DriverConfigs.Impl;

namespace SpecFlowProject2.Drivers
{
    class SeleniumDriver
    {
        private static IWebDriver webDriver;
        
        //  private RemoteWebDriver remoteWebDriver;

        private readonly ScenarioContext _scenarioContext;
        static string basePath = AppDomain.CurrentDomain.BaseDirectory.Remove(AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin"));
        public SeleniumDriver(ScenarioContext scenarioContext){
            _scenarioContext = scenarioContext;
        }

        public IWebDriver Setup() {

            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());

            webDriver = new ChromeDriver();

            _scenarioContext.Set(webDriver, "WebDriver");

            webDriver.Manage().Window.Maximize();

            return webDriver;
        }

        public IWebDriver SetupRemmote(string browserName)
        {
            
             dynamic Options =  GetBrowserOptions(browserName);
             
             webDriver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub/"), Options.ToCapabilities());
            
             _scenarioContext.Set(webDriver, "WebDriver");

             webDriver.Manage().Window.Maximize();
            
             return webDriver;
        }

        public static ISearchContext _Driver
        {
            get { return webDriver; }
        }
        private dynamic GetBrowserOptions(string browserName) {

            if (browserName.ToLower() == "chrome")
                return new ChromeOptions();
            if (browserName.ToLower() == "firefox")
                return new FirefoxOptions();

            return new ChromeOptions();
        }


    }
}
