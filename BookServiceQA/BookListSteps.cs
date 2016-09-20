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

namespace BookServiceQA
{
    [Binding]
    public class BookListSteps
    {
        [Given]
        public void Given_I_have_accessed_the_BJSS_book_store()
        {
            //driver = new ChromeDriver();
            //driver = new FirefoxDriver();
            //IWebDriver driver = new InternetExplorerDriver(@"C:\C#.Pluralsight\Selenium\lib"); 
            Browser.Driver().Navigate().GoToUrl("https://localhost:44302");
            Assert.That(Browser.Driver().FindElement(By.TagName("h1")).Text,
                Is.EqualTo("BJSS Book Store Test Environments"));
        }

        [Given]
        public void Given_at_least_one_test_environment_exist()
        {
            Assert.That(Browser.Driver().FindElement(By.LinkText("Details")), Is.Not.Null);
        }

        [When]
        public void When_I_click_on_the_Details_link()
        {
            Browser.Driver().FindElement(By.LinkText("Details")).Click();
        }

        [Then]
        public void Then_book_list_is_displayed()
        {
            Browser.AmOnTheBookList();

            String windowTitle = Browser.Driver().Title;
            Assert.That(windowTitle, Is.EqualTo("BJSS Book Store"));    //Window title

            String pageHeader = Browser.Driver().FindElement(By.TagName("h1")).Text;
            Assert.That(pageHeader, Is.EqualTo("BJSS Book Store"));    //Page header

            String bookSection = Browser.Driver().FindElements((By.CssSelector("h2")))[0].Text;
            Assert.That(bookSection, Is.EqualTo("Books"));              //Books frame
        }
        

    }
}
