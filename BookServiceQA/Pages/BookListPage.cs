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

        //[Find(By.TagName("h1"))]
        public String windowTitle;
        public String pageHeader;
        public String bookSection;

        public BookListPage()
        {
            windowTitle = Browser.Driver().Title;
            pageHeader = Browser.Driver().FindElement(By.TagName("h1")).Text;
            bookSection = Browser.Driver().FindElements((By.CssSelector("h2")))[0].Text;
            books = Browser.Driver().FindElements(By.LinkText("Details"));            
        }

        public void ClickBook(int num)
        {
            books[num].Click();
        }
    }
}
