using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BookServiceQA
{
    public static class Browser
    {
        private static IWebDriver driver;
        public static string TestURL;

        public static IWebDriver Driver()
        {
            TestURL = ConfigurationManager.AppSettings["TestURL"];
            if (driver == null)
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("--disable-extensions");
                driver = new ChromeDriver(options);
            }
            return driver;
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
        
        public static void AmOnTheEnvList()
        {
            string pageHeader = "";
            int i = 0;
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
            } while (pageHeader != "BJSS Book Store Test Environments" && i <= 100);
        }

        public static void BookListIsLoaded()
        {
            int bookCount = 0;
            int i = 0;
            do
            {
                try
                {
                    bookCount = Driver().FindElements(By.LinkText("Details")).Count;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                Thread.Sleep(100);
                i++;
            } while (bookCount <= 0 && i <= 100);
        }

        public static void goToEnvList()
        {
            Driver().Navigate().GoToUrl(TestURL);
            }

        public static void goToBookList()
        {
            goToEnvList();
            Driver().FindElement(By.LinkText("Details")).Click();
            //AmOnTheBookList();
        }
    }
}
