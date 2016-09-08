using System;
using System.Threading;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

namespace BookServiceQA
{
    [Binding]
    public class BookDetailsSteps
    {
        public IWebDriver driver;
        private string authorName;
        private string titleName;

        [Given]
        public void Given_I_am_on_the_Book_list_screen()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://localhost:44302");
            driver.FindElement(By.LinkText("Details")).Click();
            Thread.Sleep(2000);
            String pageHeader = driver.FindElement(By.TagName("h1")).Text;
            Assert.That(pageHeader, Is.EqualTo("BJSS Book Store"));    //Page header
        }
        
        [Given]
        public void Given_at_least_one_book_exist_in_the_system()
        {
            Assert.That(driver.FindElements(By.LinkText("Details")).Count, Is.GreaterThan(0));
            authorName =
                driver.FindElements(By.ClassName("list-unstyled"))[0].FindElements(By.TagName("li"))[0].FindElements(
                    By.TagName("span"))[0].Text;    // Fetching author from book list frame
            titleName =
                driver.FindElements(By.ClassName("list-unstyled"))[0].FindElements(By.TagName("li"))[0].FindElements(
                    By.TagName("span"))[1].Text;    // Fetching title from book list frame
        }
        
        [Given, When]
        public void I_click_on_Details()
        {
            driver.FindElements(By.LinkText("Details"))[0].Click();
        }
        
        [When]
        public void When_I_click_on_the_Home_link()
        {
            driver.FindElement((By.LinkText("Home"))).Click();
        }
        
        [Then]
        public void Then_the_Author_field_is_displayed()
        {
            string firstTableField = driver.FindElements(By.TagName("table"))[0].FindElements(By.TagName("td"))[0].Text;
            Assert.That(firstTableField, Is.EqualTo("Author"));
        }
        
        [Then]
        public void Then_the_Title_field_is_displayed()
        {
            string secondTableField = driver.FindElements(By.TagName("table"))[0].FindElements(By.TagName("td"))[2].Text;
            Assert.That(secondTableField, Is.EqualTo("Title"));
        }
        
        [Then]
        public void Then_the_Year_field_is_displayed()
        {
            string secondTableField = driver.FindElements(By.TagName("table"))[0].FindElements(By.TagName("td"))[4].Text;
            Assert.That(secondTableField, Is.EqualTo("Year"));
        }
        
        [Then]
        public void Then_the_Genre_field_is_displayed()
        {
            string secondTableField = driver.FindElements(By.TagName("table"))[0].FindElements(By.TagName("td"))[6].Text;
            Assert.That(secondTableField, Is.EqualTo("Genre"));
        }
        
        [Then]
        public void Then_the_Price_field_is_displayed()
        {
            string secondTableField = driver.FindElements(By.TagName("table"))[0].FindElements(By.TagName("td"))[8].Text;
            Assert.That(secondTableField, Is.EqualTo("Price"));
        }

        [Then]
        public void Then_the_Book_List_Author_matches_with_the_Detail_Author()
        {
            string secondTableField = driver.FindElements(By.TagName("table"))[0].FindElements(By.TagName("td"))[1].Text;
            Assert.That(secondTableField, Is.EqualTo(authorName));
        }

        [Then]
        public void Then_the_Book_List_Title_matches_with_the_Detail_Title()
        {

            string secondTableField = driver.FindElements(By.TagName("table"))[0].FindElements(By.TagName("td"))[3].Text;
            Assert.That(secondTableField, Is.EqualTo(titleName));
        }

        [Then]
        public void Then_the_Detail_frame_is_YES_NO_displayed(string yes_no)
        {
            Thread.Sleep(4000);
            //driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            if (yes_no.Equals("not"))
            {
                Assert.That(driver.FindElements(By.ClassName("panel-title"))[1].Text, Is.Not.EqualTo("Detail"));
            }
            else
            {
                Assert.That(driver.FindElements(By.ClassName("panel-title"))[1].Text, Is.EqualTo("Detail"));
            }
        }

        /*
        [AfterScenario]
        public void CloseBrowser()
        {
            driver.Quit();
        }*/
    }
}
