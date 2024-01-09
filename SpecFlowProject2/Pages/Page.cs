using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SpecFlowProject2.Drivers;
using TechTalk.SpecFlow;

namespace SpecFlowProject2.Pages
{
    public static class Page
    {

        public static IWebDriver driver;
        private static readonly ScenarioContext _scenarioContext;
        private static T GetPage<T>() where T : new()
        {
            var page = new T();
            PageFactory.InitElements(SeleniumDriver._Driver, page);
            return page;
        }

        public static Homepage Home
        {
            get { return GetPage<Homepage>(); }
        }

        public static GoogleHome GoHome
        {
            get { return GetPage<GoogleHome>(); }
        }

    }
}
