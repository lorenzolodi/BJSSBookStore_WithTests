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

        //[FindsBy(How = How.TagName, Using = "table")]
        //public IList<IWebElement> Table;

        [FindsBy(How = How.TagName, Using = "td")]
        public IList<IWebElement> TableCells;

        public String AuthorLabel;
        public String Author;
        public String TitleLabel;
        public String Title;
        public String Yearlabel;
        public String Year;
        public String GenreLabel;
        public String Genre;
        public String PriceLabel;
        public String Price;

        public BookDetailsPagePF(IWebDriver driver)
        {
            System.Threading.Thread.Sleep(500);
            PageFactory.InitElements(driver, this);
            AuthorLabel = TableCells[0].Text;
            Author = TableCells[1].Text;
            TitleLabel = TableCells[2].Text;
            Title = TableCells[3].Text;
            Yearlabel = TableCells[4].Text;
            Year = TableCells[5].Text;
            GenreLabel = TableCells[6].Text;
            Genre = TableCells[7].Text;
            PriceLabel = TableCells[8].Text;
            Price = TableCells[9].Text;
        }
    }
}
