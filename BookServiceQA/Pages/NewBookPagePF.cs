using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BookServiceQA.Pages
{
    class NewBookPagePF
    {
        [FindsBy(How = How.Id, Using = "inputTitle")]
        public IWebElement TitleTextBox;

        [FindsBy(How = How.Id, Using = "inputYear")]
        public IWebElement YearTextBox;

        [FindsBy(How = How.Id, Using = "inputGenre")]
        public IWebElement GenreTextBox;

        [FindsBy(How = How.Id, Using = "inputPrice")]
        public IWebElement PriceTextBox;

        [FindsBy(How = How.CssSelector, Using = "select")]
        public IWebElement DropDown;

        [FindsBy(How = How.CssSelector, Using = "form[class=\"form-horizontal\"]>button")]
        public IWebElement Submit;

        public SelectElement AuthorDropdownElement
        {
            get { return new SelectElement(DropDown); }
        }

        public NewBookPagePF(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }
    }
}
