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

        [FindsBy(How = How.LinkText, Using = "Details")]
        public IList<IWebElement> Details;

        //[FindsBy(How = How.ClassName, Using = ".page-header")]
        public String windowTitle;

        [FindsBy(How = How.CssSelector, Using = ".page-header > h1")]
        public IWebElement pageHeader;

        //public String bookSection;
        [FindsBy(How = How.CssSelector, Using = ".panel-heading > h2")]
        public IWebElement bookSection;

        public BookListPagePF(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
            windowTitle = Browser.Driver().Title;          
        }

        public BookDetailsPagePF ClickBook(int num)
        {
            Details[num-1].Click();
            return new BookDetailsPagePF(Browser.Driver());
        }

        public string Author(int n)
        {
            return Browser.Driver().FindElements(By.ClassName("list-unstyled"))[0].FindElements(By.CssSelector("li>strong>span"))[n].Text;
        }

        public string Title(int n)
        {
            return Browser.Driver().FindElements(By.ClassName("list-unstyled"))[0].FindElements(By.CssSelector("li>span"))[n].Text;
        }
    }
}
