using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;

namespace BookServiceQA.Pages
{
    public class BookListPage
    {
        ReadOnlyCollection<IWebElement> books;
        public string windowTitle;
        public string pageHeader;
        public string bookSection;

        public BookListPage()
        {
            String windowTitle = Browser.Driver().Title;
            String pageHeader = Browser.Driver().FindElement(By.TagName("h1")).Text;
            String bookSection = Browser.Driver().FindElements((By.CssSelector("h2")))[0].Text;
            books = Browser.Driver().FindElements(By.LinkText("Details"));
        }

        public void ClickBook(int num)
        {
            books[num].Click();
        }
    }
}
