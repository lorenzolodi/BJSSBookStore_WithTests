using System;
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
        private IWebDriver driver;

        [Given]
        public void Given_I_have_accessed_the_BJSS_book_store()
        {
            driver = new ChromeDriver();
            //driver = new FirefoxDriver();
            //IWebDriver driver = new InternetExplorerDriver(@"C:\C#.Pluralsight\Selenium\lib"); 
            driver.Navigate().GoToUrl("https://localhost:44302");
        }

        [Given]
        public void Given_at_least_one_test_environment_exist()
        {
            Assert.That(driver.FindElement(By.LinkText("Details")), Is.Not.Null);
        }
        
        [When]
        public void When_I_click_on_the_Details_link()
        {
            driver.FindElement(By.LinkText("Details")).Click();
        }
        
        [Then]
        public void Then_book_list_is_displayed()
        {
            Thread.Sleep(2000);
            String windowTitle = driver.Title;
            Assert.That(windowTitle, Is.EqualTo("BJSS Book Store"));    //Window title

            String pageHeader = driver.FindElement(By.TagName("h1")).Text;  
            Assert.That(pageHeader, Is.EqualTo("BJSS Book Store") );    //Page header

            String bookSection = driver.FindElements((By.CssSelector("h2")))[0].Text;
            Assert.That(bookSection, Is.EqualTo("Books"));              //Books frame
        }
        /*
        [AfterScenario]
        public void CloseBrowser()
        {
            driver.Quit();
        }*/
    }
}
