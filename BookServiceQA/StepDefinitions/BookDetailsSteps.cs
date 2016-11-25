using System;
using System.Threading;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using BookServiceQA.Pages;

namespace BookServiceQA
{
    [Binding] 
    public class BookDetailsSteps
    {
        private string authorName;
        private string titleName;
        BookListPagePF blPage;
        BookDetailsPagePF bookDetails;
        TestEnvironmentsPagePF envPage;

        [When]
        public void When_I_click_on_the_Home_link()
        {
            envPage = bookDetails.ClickHome();
        }


        [Given]
        public void Given_I_am_on_the_Book_list_screen()
        {
            Browser.goToBookList();
            //Browser.AmOnTheBookList();
            Browser.BookListIsLoaded();
            blPage = new BookListPagePF(Browser.Driver());
        }
        
        [Given]
        public void Given_at_least_one_book_exist_in_the_system()
        {
            Assert.That(blPage.books.Count, Is.GreaterThan(0));
            authorName = blPage.Author(1);
            titleName = blPage.Title(1);
        }
        
        [Given, When]
        public void I_select_a_book()
        {//I click on a book Details
            bookDetails = blPage.ClickBook(1);
        }

        [Then]
        public void Then_the_Author_field_is_displayed()
        {
            Assert.That(bookDetails.AuthorLabel.Text, Is.EqualTo("Author"));
        }
        
        [Then]
        public void Then_the_Title_field_is_displayed()
        {
            Assert.That(bookDetails.TitleLabel.Text, Is.EqualTo("Title"));
        }
        
        [Then]
        public void Then_the_Year_field_is_displayed()
        {
            Assert.That(bookDetails.Yearlabel.Text, Is.EqualTo("Year"));
        }
        
        [Then]
        public void Then_the_Genre_field_is_displayed()
        {
            Assert.That(bookDetails.GenreLabel.Text, Is.EqualTo("Genre"));
        }
        
        [Then]
        public void Then_the_Price_field_is_displayed()
        {
            Assert.That(bookDetails.PriceLabel.Text, Is.EqualTo("Price"));
        }

        [Then]
        public void Then_the_Book_List_Author_matches_with_the_Detail_Author()
        {
            Assert.That(bookDetails.Author.Text, Is.EqualTo(blPage.Author(0)));
        }

        [Then]
        public void Then_the_Book_List_Title_matches_with_the_Detail_Title()
        {
            Assert.That(bookDetails.Title.Text, Is.EqualTo(blPage.Title(0)));
        }

        [Then]
        public void Then_the_book_details_are_YES_NO_displayed(string yes_no)
        {//The detail frame is (not) displayed
            if (yes_no.Equals("not"))
            {
                Assert.That(Browser.Driver().FindElements(By.ClassName("panel-title")).Count, Is.LessThanOrEqualTo(2));
            }
            else
            {
                WebDriverWait wait = new WebDriverWait(Browser.Driver(), TimeSpan.FromSeconds(5));
                var DetailFrame = wait.Until(d =>
                {
                    var headers = Browser.Driver().FindElements(By.CssSelector("h2[class=\"panel-title\"]"));
                    if (headers.Count == 3)
                        return headers[1];
                    return null;
                });

                Assert.That(DetailFrame.Text, Is.EqualTo("Detail"));
                Assert.That(bookDetails.AuthorLabel.Text, Is.EqualTo("Author"));
                Assert.That(bookDetails.TitleLabel.Text, Is.EqualTo("Title"));
                Assert.That(bookDetails.Yearlabel.Text, Is.EqualTo("Year"));
                Assert.That(bookDetails.GenreLabel.Text, Is.EqualTo("Genre"));
                Assert.That(bookDetails.PriceLabel.Text, Is.EqualTo("Price"));
            }
        }

        [Given]
        public void Given_a_list_of_books()
        {
            Browser.goToBookList();
            Browser.BookListIsLoaded();
            blPage = new BookListPagePF(Browser.Driver());
        }

        [Then]
        public void Then_I_can_see_its_details()
        {
            WebDriverWait wait = new WebDriverWait(Browser.Driver(), TimeSpan.FromSeconds(5));
            var DetailFrame = wait.Until(d =>
            {
                var headers = Browser.Driver().FindElements(By.CssSelector("h2[class=\"panel-title\"]"));
                if (headers.Count == 3)
                    return headers[1];
                return null;
            });

            Assert.That(DetailFrame.Text, Is.EqualTo("Detail"));
            Assert.That(bookDetails.AuthorLabel.Text, Is.EqualTo("Author"));
            Assert.That(bookDetails.TitleLabel.Text, Is.EqualTo("Title"));
            Assert.That(bookDetails.Yearlabel.Text, Is.EqualTo("Year"));
            Assert.That(bookDetails.GenreLabel.Text, Is.EqualTo("Genre"));
            Assert.That(bookDetails.PriceLabel.Text, Is.EqualTo("Price"));
        }

        [Then]
        public void Then_I_can_see_the_matching_Author_and_Title()
        {
            Assert.That(bookDetails.Author.Text, Is.EqualTo(blPage.Author(0)));
            Assert.That(bookDetails.Title.Text, Is.EqualTo(blPage.Title(0)));
        }

    }
}
