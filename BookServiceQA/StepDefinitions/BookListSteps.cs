using System;
using System.Diagnostics;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.IE;
using BookServiceQA.Pages;

namespace BookServiceQA
{
    [Binding]
    public class BookListSteps
    {
        TestEnvironmentsPagePF tePage;
        BookListPagePF blPage;

        [Given]
        public void Given_I_have_accessed_the_BJSS_book_store()
        {
            Browser.Driver().Navigate().GoToUrl("https://localhost:44302");
            tePage = new TestEnvironmentsPagePF();
            Assert.That(tePage.pageHeader, Is.EqualTo("BJSS Book Store Test Environments"));
            //Assert.That(Browser.Driver().FindElement(By.TagName("h1")).Text,
            //    Is.EqualTo("BJSS Book Store Test Environments"));
        }

        [Given]
        public void Given_at_least_one_test_environment_exist()
        {
            Assert.That(tePage.Env, Is.Not.Null);
            //Assert.That(Browser.Driver().FindElement(By.LinkText("Details")), Is.Not.Null);
        }

        [When]
        public void When_I_click_on_the_Details_link()
        {
            blPage = tePage.SelectEnvironment(0);
        }

        [Then]
        public void Then_book_list_is_displayed()
        {
            Browser.AmOnTheBookList(); //Wait

            //BookListPage blPage = new BookListPage();
            Assert.That(blPage.windowTitle , Is.EqualTo("BJSS Book Store"));    //Window title

            //String pageHeader = Browser.Driver().FindElement(By.TagName("h1")).Text;
            Assert.That(blPage.pageHeader, Is.EqualTo("BJSS Book Store"));    //Page header

            //String bookSection = Browser.Driver().FindElements((By.CssSelector("h2")))[0].Text;
            Assert.That(blPage.bookSection, Is.EqualTo("Books"));              //Books frame
        }

        //[Test]  // TZ
        //public void Page_object_test()
        //{
        //    BookListPagePF page = new BookListPagePF(Browser.Driver());
        //    Assert.That(page.pageHeader.Text, Is.EqualTo("BJSS Book Store"));
        //    Assert.That(page.books.Count, Is.EqualTo(55));
        //}


    }
}
