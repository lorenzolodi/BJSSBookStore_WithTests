using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using OpenQA.Selenium.Support.PageObjects;

namespace BookServiceQA.Pages
{
    public class BookListPagePF
    {
        [FindsBy(How = How.CssSelector, Using =".list-unstyled li")]
        public IList<IWebElement> books;

        //[FindsBy(How = How.ClassName, Using = ".page-header")]
        public String windowTitle;

        [FindsBy(How = How.CssSelector, Using = ".page-header > h1")]
        public IWebElement pageHeader;

        //public String bookSection;
        [FindsBy(How = How.CssSelector, Using = ".panel-title > h2")]
        public IWebElement bookSection;

        public BookListPagePF(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
            //driver.Navigate().GoToUrl("https://localhost:44302/Home/Environment/0f4f1173-6f4d-4b98-838d-435905bcc8ee");
            windowTitle = Browser.Driver().Title;          
        }

        public void ClickBook(int num)
        {
            //books[num].Click();
        }
    }
}
