using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BookServiceQA
{
    public static class BrowserDriver
    {
        public static IWebDriver driver;
        
        public static void GoHome()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://localhost:44302");
            driver.FindElement(By.LinkText("Details")).Click();
            Thread.Sleep(2000);
        }
    }
}
