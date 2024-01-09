using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using SpecFlowProject2.Drivers;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using TechTalk.SpecFlow;

namespace SpecFlowProject2.Hooks
{
    [Binding]
    public sealed class HooksInitialization
    {
        private static ExtentTest featureName;
        public static ExtentTest scenario;
        private static ExtentReports extent;
        public static string ReportPath;
        static string basePath = AppDomain.CurrentDomain.BaseDirectory.Remove(AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin"));
        private static string featureNameFolder;
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
       // static Process p;
        private static string screenShotType;

        private readonly ScenarioContext _scenarioContext;
        public HooksInitialization(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        
        public String TakeScreenshot(String name)
        {
            string finalName = name.Replace("\"", "");
            string time = DateTime.Now.ToString().Replace(":","");
            string subdir = basePath + "\\Screenshots\\" + featureNameFolder;
            if (!Directory.Exists(subdir))
            {
                Directory.CreateDirectory(subdir);
            }
            string path = subdir+"\\"+ finalName +"_"+time+ ".png";
            IWebDriver driver = _scenarioContext.Get<IWebDriver>("WebDriver");
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(path, ScreenshotImageFormat.Png);
            return path;
        }


        public static void ExecuteBatFile(string path, string fileName)
        {
            try
            {
                Process proc = null;
                string _batDir = string.Format(path);
                proc = new Process();
                proc.StartInfo.WorkingDirectory = _batDir;
                proc.StartInfo.FileName = fileName;
                proc.StartInfo.CreateNoWindow = false;
                proc.Start();
                proc.WaitForExit();
                proc.Close();
            }
            catch (Win32Exception w)
            {
                Console.WriteLine(w.Message);
            }
           
        }


        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            string reportPath = basePath + "Report\\index.html";
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            htmlReporter.LoadConfig(basePath + "extent-config.xml");
            screenShotType = htmlReporter.MasterConfig.GetValue("screenshotType");
          //  p = System.Diagnostics.Process.Start(basePath + "Executables\\start.bat");
            
            Thread.Sleep(10000);
            
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            //Create dynamic feature name
            featureName = extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
            featureNameFolder = featureContext.FeatureInfo.Title.ToString();
            Console.WriteLine("BeforeFeature");

        }


        [BeforeScenario]
        public void BeforeScenario()
        {
            SeleniumDriver seleniumDriver = new SeleniumDriver(_scenarioContext);
            _scenarioContext.Set(seleniumDriver, "SeleniumDriver");
            HooksInitialization hooksInitialization = new HooksInitialization(_scenarioContext);
            _scenarioContext.Set(hooksInitialization, "HooksInitialization");
            //TODO: implement logic that has to run before executing each scenario
            scenario = featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);

        }


        [AfterStep]
        public void InsertReportingSteps()
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();

            if (_scenarioContext.TestError == null && ScenarioStepContext.Current.Status.ToString().ToLower()=="ok")
            {
                MediaEntityModelProvider media =null;
                if (screenShotType == "screenshotOnlyOnPass" || screenShotType == "screenshotOnlyOnBoth")
                {
                    media = MediaEntityBuilder.CreateScreenCaptureFromPath(TakeScreenshot(ScenarioStepContext.Current.StepInfo.Text)).Build();
                }
                if (stepType == "Given")
                {
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Pass("Pass", media);
                }
                else if (stepType == "When")
                {
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Pass("Pass", media);
                }
                else if (stepType == "Then")
                {
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Pass("Pass", media);
                }
                else if (stepType == "And")
                {
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Pass("Pass", media);
                }
                extent.Flush();
            }
            else if(_scenarioContext.TestError != null)
            {
                Exception exception = null ;
                MediaEntityModelProvider media1 = null;
                if (screenShotType == "screenshotOnlyOnFail" || screenShotType== "screenshotOnlyOnBoth")
                {
                    try
                    {
                        media1 = MediaEntityBuilder.CreateScreenCaptureFromPath(TakeScreenshot(ScenarioStepContext.Current.StepInfo.Text)).Build();
                    }
                    catch (Exception e) {
                        Console.WriteLine(e.Message);
                        exception = e;
                    }
                    
                }
                    if (exception != null)
                    {
                        if (stepType == "Given")
                        {
                            scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message, media1);
                        }
                        else if (stepType == "When")
                        {
                            scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message, media1);
                        }
                        else if (stepType == "Then")
                        {
                            scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message, media1);
                        }
                        else if (stepType == "And")
                        {
                            scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message, media1);
                        }

                    }
                    else {
                        if (stepType == "Given")
                        {
                            scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
                        }
                        else if (stepType == "When")
                        {
                            scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
                        }
                        else if (stepType == "Then")
                        {
                            scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
                        }
                        else if (stepType == "And")
                        {
                            scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
                        }
                }
                
                extent.Flush();
            }
            else
            {
                if (stepType == "Given")
                {
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Warning(_scenarioContext.StepContext.Status.ToString());
                }
                else if (stepType == "When")
                {
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Warning(_scenarioContext.StepContext.Status.ToString());
                }
                else if (stepType == "Then")
                {
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Warning(_scenarioContext.StepContext.Status.ToString());
                }
                else if (stepType == "And")
                {
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Warning(_scenarioContext.StepContext.Status.ToString());
                }
                extent.Flush();
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _scenarioContext.Get<IWebDriver>("WebDriver").Quit();
        }

        [AfterTestRun]
        public static  void AfterTestRun()
        {
           
          // Array.ForEach(Process.GetProcessesByName("cmd"), x => x.Kill());
            //kill the browser
            //Flush report once test completes
            extent.Flush();
        }


    }
}
