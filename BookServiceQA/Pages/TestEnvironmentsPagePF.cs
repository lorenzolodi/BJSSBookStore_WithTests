using NUnit;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.PageObjects;

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
            PageFactory.InitElements(driver, this);
        }

        public BookListPagePF SelectEnvironment(int num)
        {
            Env[num].Click();
            return new BookListPagePF(Browser.Driver());
        }
    }
}
