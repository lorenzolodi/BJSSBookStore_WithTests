using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;

namespace BookServiceQA.Pages
{
    public class BookDetailsPagePF
    {
        [FindsBy(How = How.CssSelector, Using = ".panel - heading > h2")]
        public IList<IWebElement> PageSections;

        [FindsBy(How = How.TagName, Using = "td")]
        public IList<IWebElement> TableCells;

        [FindsBy(How = How.LinkText, Using = "Home")]
        public IWebElement HomeLink;

        public IWebElement AuthorLabel;
        public IWebElement Author;
        public IWebElement TitleLabel;
        public IWebElement Title;
        public IWebElement Yearlabel;
        public IWebElement Year;
        public IWebElement GenreLabel;
        public IWebElement Genre;
        public IWebElement PriceLabel;
        public IWebElement Price;

        public BookDetailsPagePF(IWebDriver driver)
        {
            System.Threading.Thread.Sleep(500);
            PageFactory.InitElements(driver, this);
            AuthorLabel = TableCells[0];
            Author = TableCells[1];
            TitleLabel = TableCells[2];
            Title = TableCells[3];
            Yearlabel = TableCells[4];
            Year = TableCells[5];
            GenreLabel = TableCells[6];
            Genre = TableCells[7];
            PriceLabel = TableCells[8];
            Price = TableCells[9];
        }

        public TestEnvironmentsPagePF ClickHome()
        {
            HomeLink.Click();
            return new TestEnvironmentsPagePF(Browser.Driver());
        }
    }
}
