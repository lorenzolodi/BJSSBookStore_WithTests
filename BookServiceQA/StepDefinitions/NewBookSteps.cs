using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using BookServiceQA.Pages;

namespace BookServiceQA
{
    [Binding]
    public class NewBookSteps
    {
        int bookCount;
        string newAuthor;
        string newTitle;
        NewBookPagePF nbPage;
        BookListPagePF blPage;
        ErrorMessagePF ErrorMessage;

        [Given]
        public void Given_I_am_on_the_Book_list_page()
        {
            Browser.goToBookList();

            blPage = new BookListPagePF(Browser.Driver());
            nbPage = new NewBookPagePF(Browser.Driver());

            //Browser.AmOnTheBookList();
        }


        [Given]
        public void Given_I_have_entered_the_following_values_on_the_Add_Book_form(Table table)
        {
            newAuthor = table.Rows[0]["Author"];    // Values saved for the assert
            newTitle = table.Rows[0]["Title"];

            nbPage.AuthorDropdownElement.SelectByText(table.Rows[0]["Author"]);
            nbPage.TitleTextBox.SendKeys(table.Rows[0]["Title"]);
            nbPage.YearTextBox.SendKeys(table.Rows[0]["Year"]);
            nbPage.GenreTextBox.SendKeys(table.Rows[0]["Genre"]);
            nbPage.PriceTextBox.SendKeys(table.Rows[0]["Price"]);
        }

        [Given(@"I have entered (.*), (.*), (.*), (.*) and (.*)")]
        public void GivenIHaveEnteredAnd(string author, string title, string year, string genre, string price)
        {
            nbPage.AuthorDropdownElement.SelectByText(author);
            if (title != "n/a")
            {
                nbPage.TitleTextBox.SendKeys(title);
            }
            if (year != "n/a")
            {
                nbPage.YearTextBox.SendKeys(year);
            }
            else
            {
                nbPage.YearTextBox.SendKeys("1000");
            }
            if (price != "n/a")
            {
                nbPage.PriceTextBox.SendKeys(price);
            }
            else
            {
                nbPage.PriceTextBox.SendKeys("100");
            }
            nbPage.GenreTextBox.SendKeys(genre);
            if (year == "n/a")
            {
                nbPage.YearTextBox.Clear();
            }
            if (price == "n/a")
            {
                nbPage.PriceTextBox.Clear();
            }
        }

        [When]
        public void When_I_press_Submit()
        {
            bookCount = blPage.books.Count;
            nbPage.Submit.Click();
        }

        [Then]
        public void Then_the_new_book_is_displayed_in_the_book_list()
        {
            WebDriverWait wait = new WebDriverWait(Browser.Driver(), TimeSpan.FromSeconds(5));
            wait.Until(d =>
            {
                var elements =
                    Browser.Driver()
                        .FindElement(By.CssSelector("div[class=\"panel-body\"]>ul"))
                        .FindElements(By.TagName("li"));
                if (elements.Count > bookCount)
                {
                    return elements[0];
                }
                return null;
            });
            
            Assert.That(blPage.Author(bookCount), Is.EqualTo(newAuthor));
            Assert.That(blPage.Title(bookCount), Is.EqualTo(newTitle));
        }
        
        [Then]
        public void Then_I_will_not_be_able_to_add_the_book()
        {
            Assert.That(blPage.books.Count, Is.EqualTo(bookCount));
        }
        
        [Then]
        public void Then_an_error_message_will_be_displayed()
        {
            ErrorMessage = new ErrorMessagePF(Browser.Driver());
            Assert.That(ErrorMessage.ErrorText.Text, Is.EqualTo("Bad Request"));
        }
    }
}
