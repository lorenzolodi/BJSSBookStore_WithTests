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
            Browser.goToEnvList();
            tePage = new TestEnvironmentsPagePF(Browser.Driver());
            Assert.That(tePage.pageHeader.Text, Is.EqualTo("BJSS Book Store Test Environments"));
        }

        [Given]
        public void Given_at_least_one_test_environment_exist()
        {
            Assert.That(tePage.Env.Count, Is.GreaterThan(0));
        }

        [When]
        public void When_I_select_an_environment()
        {//Click on the environment Details link
            blPage = tePage.SelectEnvironment(0);
        }

        [Then]
        public void Then_book_list_is_displayed()
        {
            //Browser.AmOnTheBookList(); //Wait
            
            Assert.That(blPage.windowTitle , Is.EqualTo("BJSS Book Store"));    //Window title
            
            Assert.That(blPage.pageHeader.Text, Is.EqualTo("BJSS Book Store"));    //Page header
            
            Assert.That(blPage.bookSection.Text, Is.EqualTo("Books"));              //Books frame
        }

        [Then]
        public void Then_the_environment_list_screen_is_displayed()
        {
            //Browser.AmOnTheEnvList();   
            Assert.That(tePage.pageHeader.Text, Is.EqualTo("BJSS Book Store Test Environments"));
        }
    }
}
