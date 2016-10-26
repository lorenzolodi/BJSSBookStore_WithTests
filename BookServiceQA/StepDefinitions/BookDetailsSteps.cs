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

        [Given]
        public void Given_I_am_on_the_Book_list_screen()
        {
            Browser.Driver().Navigate().GoToUrl("https://localhost:44302");
            Browser.Driver().FindElement(By.LinkText("Details")).Click();
            Browser.AmOnTheBookList();

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
        public void I_click_on_Details()
        {
            bookDetails = blPage.ClickBook(1);
        }
        
        [When]
        public void When_I_click_on_the_Home_link()
        {
            Browser.Driver().FindElement((By.LinkText("Home"))).Click();
        }
        
        [Then]
        public void Then_the_Author_field_is_displayed()
        {
            //string firstTableField = Browser.Driver().FindElements(By.TagName("table"))[0].FindElements(By.TagName("td"))[0].Text;
            //Assert.That(firstTableField, Is.EqualTo("Author"));
            Assert.That(bookDetails.AuthorLabel, Is.EqualTo("Author"));
        }
        
        [Then]
        public void Then_the_Title_field_is_displayed()
        {
            //string secondTableField = Browser.Driver().FindElements(By.TagName("table"))[0].FindElements(By.TagName("td"))[2].Text;
            //Assert.That(secondTableField, Is.EqualTo("Title"));
            Assert.That(bookDetails.TitleLabel, Is.EqualTo("Title"));
        }
        
        [Then]
        public void Then_the_Year_field_is_displayed()
        {
            //string secondTableField = Browser.Driver().FindElements(By.TagName("table"))[0].FindElements(By.TagName("td"))[4].Text;
            //Assert.That(secondTableField, Is.EqualTo("Year"));
            Assert.That(bookDetails.Yearlabel, Is.EqualTo("Year"));
        }
        
        [Then]
        public void Then_the_Genre_field_is_displayed()
        {
            //string secondTableField = Browser.Driver().FindElements(By.TagName("table"))[0].FindElements(By.TagName("td"))[6].Text;
            //Assert.That(secondTableField, Is.EqualTo("Genre"));
            Assert.That(bookDetails.GenreLabel, Is.EqualTo("Genre"));
        }
        
        [Then]
        public void Then_the_Price_field_is_displayed()
        {
            //string secondTableField = Browser.Driver().FindElements(By.TagName("table"))[0].FindElements(By.TagName("td"))[8].Text;
            //Assert.That(secondTableField, Is.EqualTo("Price"));
            Assert.That(bookDetails.PriceLabel, Is.EqualTo("Price"));
        }

        [Then]
        public void Then_the_Book_List_Author_matches_with_the_Detail_Author()
        {
            //string secondTableField = Browser.Driver().FindElements(By.TagName("table"))[0].FindElements(By.TagName("td"))[1].Text;
            //Assert.That(secondTableField, Is.EqualTo(authorName));
            Assert.That(bookDetails.Author, Is.EqualTo(blPage.Author(1)));
        }

        [Then]
        public void Then_the_Book_List_Title_matches_with_the_Detail_Title()
        {
            //string secondTableField = Browser.Driver().FindElements(By.TagName("table"))[0].FindElements(By.TagName("td"))[3].Text;
            //Assert.That(secondTableField, Is.EqualTo(titleName));
            Assert.That(bookDetails.Title, Is.EqualTo(blPage.Title(1)));
        }

        [Then]
        public void Then_the_Detail_frame_is_YES_NO_displayed(string yes_no)
        {

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
            }
        }
    }
}
