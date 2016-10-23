using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
                //DesiredCapabilities capabilities = DesiredCapabilities.Chrome();
                //ChromeOptions options = new ChromeOptions();
                //options.AddArguments("test-type");
                //capabilities.SetCapability("chrome.binary", "C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe");
                //capabilities.SetCapability(ChromeOptions.Capability, options);



                //System.setProperty("webdriver.chrome.driver", "C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe");
                //ChromeOptions options = new ChromeOptions();
                //options.AddArguments("--test-type");
                //WebDriver driver = new ChromeDriver(options);

                IWebDriver driver = new ChromeDriver();
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
    }
}
