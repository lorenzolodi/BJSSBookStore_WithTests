using System;
using System.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace BookServiceQA
{
    public static class Browser
    {
        private static IWebDriver _driver;
        
        public static IWebDriver Driver()
        {
            if (_driver == null)
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("--disable-extensions");
                IWebDriver driver = new ChromeDriver(options);

                _driver = driver;
            }
            return _driver;
        }

        public static void AmOnTheBookList()
        {
            string pageHeader = "";
            int i=0;
            do
            {
                try
                {
                    pageHeader = Driver().FindElement(By.TagName("h1")).Text;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                Thread.Sleep(100);
            } while (pageHeader != "BJSS Book Store" && i<=100);
        }

        public static void goToEnvList()
        {
            string testURL = ConfigurationManager.AppSettings["TestURL"];
            Browser.Driver().Navigate().GoToUrl(testURL);
        }

        public static void goToBookList()
        {
            goToEnvList();
            Browser.Driver().FindElement(By.LinkText("Details")).Click();
            Browser.AmOnTheBookList();
        }
    }
}
