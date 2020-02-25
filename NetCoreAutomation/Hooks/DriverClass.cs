﻿using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Reflection;
using System.IO;

namespace NetCoreAutomation.Hooks
{
    public static class DriverClass
    {


        public static void StartTest(string BaseURL)
        {
            try
            {
                
                ChromeOptions ChromeOptions = new ChromeOptions();
                ChromeOptions.AddAdditionalCapability("useAutomationExtension", false);
                ChromeOptions.AddArguments("--start-maximized");
                ChromeOptions.AddArguments("--no-sandbox");
                //ChromeOptions.AddArgument("--headless");
                CustomBaseClass.MyDriver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ChromeOptions);
                CustomBaseClass.MyDriver.Navigate().GoToUrl(BaseURL);
              //  PageLoaded("login");
                Console.WriteLine("Browser loaded, Test Passed");
            }
            catch (Exception testInitiationException)
            {
                Console.WriteLine("Failed Initiation : {0}", testInitiationException.Message);
                //ScreenShotsClass.FailedTestCaptureScreenShot("Failed Initiation");
                Assert.Fail();
            }
        }

        // This method makes sure the page is fully loaded before test is executed
        public static void PageLoaded(string ObjectId)
        {
            try
            {
                WebDriverWait waitForElement = new WebDriverWait(CustomBaseClass.MyDriver, TimeSpan.FromSeconds(60));
                waitForElement.Until(ExpectedConditions.ElementIsVisible(By.Id(ObjectId)));
            }
            catch (Exception ErrorPageLoad)
            {
                Console.WriteLine("Taking too much time: {0}", ErrorPageLoad.Message);
                // ScreenShots.FailedTestCaptureScreenShot("Load Delay");
                throw ErrorPageLoad;
            }
        }

        // Tears down test and throws exception
        public static void CloseTest()
        {
            try
            {
                CustomBaseClass.MyDriver.Quit();
                //Process[] chromeDriverProcesses = Process.GetProcessesByName("chromedriver");

                //foreach (var chromeDriverProcess in chromeDriverProcesses)
                //{
                //    chromeDriverProcess.Kill();
                //}
                Console.WriteLine("Test Completes successfully");
            }
            catch (WebDriverException testClosingException)
            {
                Console.WriteLine("Driver Failed to close the browser: {0}", testClosingException.Message);
            }

        }


    }
}
