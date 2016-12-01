using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace BookServiceQA.Pages
{
    public class TestEnvironmentsPagePF
    {
        [FindsBy(How = How.LinkText, Using = "Details")]
        public IList<IWebElement> Env;

        [FindsBy(How = How.CssSelector, Using = ".page-header > h1")]
        public IWebElement pageHeader;

        [FindsBy(How = How.CssSelector, Using = "panel-heading > h2")]
        public IWebElement tableTitle;

        [FindsBy(How = How.TagName, Using = "th")]
        public IList<IWebElement> Subs;



        public TestEnvironmentsPagePF(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("td")));

            PageFactory.InitElements(driver, this);
        }

        public BookListPagePF SelectEnvironment(int num)
        {
            Env[num].Click();
            return new BookListPagePF(Browser.Driver());
        }
    }
}
