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

        [FindsBy(How = How.TagName, Using = "h1")]
        public IWebElement pageHeader;

        public TestEnvironmentsPagePF()
        {
            PageFactory.InitElements(Browser.Driver(), this);
        }

        public BookListPagePF SelectEnvironment(int num)
        {
            Env[num].Click();
            return new BookListPagePF(Browser.Driver());
        }
    }
}
