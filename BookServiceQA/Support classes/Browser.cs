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
        private static IWebDriver _driver;
        public static string testURL;
        public static string highestAuthorId;
        public static string highestBookId;

        public static IWebDriver Driver()
        {
            testURL = ConfigurationManager.AppSettings["TestURL"];
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
            Driver().Navigate().GoToUrl(testURL);
            }

        public static void goToBookList()
        {
            goToEnvList();
            Driver().FindElement(By.LinkText("Details")).Click();
            //AmOnTheBookList();
        }
    }
}
