using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace BookServiceQA
{
    [Binding]
    public class NewBookSteps
    {
        IWebDriver driver;
        int bookCount;
        string newAuthor;
        string newTitle;

        [Given]
        public void Given_I_am_on_the_Book_list_page()
        {
            driver = new ChromeDriver(@"chromedriver_win32");

            driver.Navigate().GoToUrl("https://localhost:44302");
            driver.FindElement(By.LinkText("Details")).Click();
            Thread.Sleep(2000);

            String pageHeader = driver.FindElement(By.TagName("h1")).Text;
            Assert.That(pageHeader, Is.EqualTo("BJSS Book Store"));    //Page header
        }


        [Given]
        public void Given_I_have_entered_the_following_values_on_the_Add_Book_form(Table table)
        {
            newAuthor = table.Rows[0]["Author"];    // Values saved for the assert
            newTitle = table.Rows[0]["Title"];

            new SelectElement(driver.FindElement(By.CssSelector("select"))).SelectByText(table.Rows[0]["Author"]);
            driver.FindElement(By.Id("inputTitle")).SendKeys(table.Rows[0]["Title"]);
            driver.FindElement(By.Id("inputYear")).SendKeys(table.Rows[0]["Year"]);
            driver.FindElement(By.Id("inputGenre")).SendKeys(table.Rows[0]["Genre"]);
            driver.FindElement(By.Id("inputPrice")).SendKeys(table.Rows[0]["Price"]);
        }

        [Given(@"I have entered (.*), (.*), (.*), (.*) and (.*)")]
        public void GivenIHaveEnteredAnd(string author, string title, string year, string genre, string price)
        {
            new SelectElement(driver.FindElement(By.CssSelector("select"))).SelectByText(author);
            if (title != "n/a")
            {
                driver.FindElement(By.Id("inputTitle")).SendKeys(title);
            }
            if (year != "n/a")
            {
                driver.FindElement(By.Id("inputYear")).SendKeys(year);
            }
            else
            {
                driver.FindElement(By.Id("inputYear")).SendKeys("1000");
            }
            if (price != "n/a")
            {
                driver.FindElement(By.Id("inputPrice")).SendKeys(price);
            }
            else
            {
                driver.FindElement(By.Id("inputPrice")).SendKeys("100");
            }
            driver.FindElement(By.Id("inputGenre")).SendKeys(genre);
            if (year == "n/a")
            {
                driver.FindElement(By.Id("inputYear")).Clear();
            }
            if (price == "n/a")
            {
                driver.FindElement(By.Id("inputPrice")).Clear();
            }
        }

        [When]
        public void When_I_press_Submit()
        {
            bookCount = driver.FindElements(By.LinkText("Details")).Count;
            driver.FindElement(By.CssSelector("form[class=\"form-horizontal\"]>button")).Click();
            Thread.Sleep(1500);
        }

        [Then]
        public void Then_the_new_book_is_displayed_in_the_book_list()
        {
            Assert.That(driver.FindElement(By.CssSelector("div[class=\"panel-body\"]>ul")).FindElements(By.TagName("li"))[bookCount].FindElements(By.TagName("span"))[0].Text, Is.EqualTo(newAuthor));
            Assert.That(driver.FindElement(By.CssSelector("div[class=\"panel-body\"]>ul")).FindElements(By.TagName("li"))[bookCount].FindElements(By.TagName("span"))[1].Text, Is.EqualTo(newTitle));
        }
        
        [Then]
        public void Then_I_will_not_be_able_to_add_the_book()
        {
            Assert.That(driver.FindElements(By.LinkText("Details")).Count, Is.EqualTo(bookCount));
        }
        
        [Then]
        public void Then_an_error_message_will_be_displayed()
        {
            Assert.That(driver.FindElement(By.CssSelector("div[class=\"alert alert-danger\"]")).Displayed);
            Assert.That(driver.FindElement(By.CssSelector("p[data-bind=\"text: error\"]")).Text, Is.EqualTo("Bad Request"));
        }
        
        [AfterScenario("NewBook")]
        private void CloseBrowser()
        {
            driver.Quit();
        }
    }
}
