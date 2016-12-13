using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using BookServiceQA.Support_classes;

namespace BookServiceQA
{
    public static class Browser
    {
        private static IWebDriver driver;
        public static string TestURL;
        static string browserType;

        public static IWebDriver Driver()
        {
            TestURL = ConfigurationManager.AppSettings["TestURL"];
            browserType = ConfigurationManager.AppSettings["Browser"];
            if (driver == null)
            {
                if (browserType == "Chrome")
                {
                    ChromeOptions options = new ChromeOptions();
                    options.AddArguments("--disable-extensions");
                    driver = new ChromeDriver(options);
                }
                else if (browserType == "IE")
                {
                    driver = new InternetExplorerDriver(ProjectLocation.FromFolder("BookServiceQA").FullPath + "\\IEDriverServer32");
                }
                else if (browserType == "FireFox")
                {
                    driver = new FirefoxDriver();
                }
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
