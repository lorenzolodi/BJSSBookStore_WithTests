using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BookServiceQA.Pages
{
    class ErrorMessagePF
    {
        [FindsBy(How = How.CssSelector, Using = "div[class=\"alert alert-danger\"]")]
        public IWebElement ErrorBanner;

        [FindsBy(How = How.CssSelector, Using = "div[class=\"alert alert-danger\"]>p")]
        public IWebElement ErrorText;

        public ErrorMessagePF(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div[class=\"alert alert-danger\"]")));

            PageFactory.InitElements(driver, this);
        }
    }
}
