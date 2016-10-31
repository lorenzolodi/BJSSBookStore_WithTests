using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;

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
            PageFactory.InitElements(driver, this);
        }
    }
}
