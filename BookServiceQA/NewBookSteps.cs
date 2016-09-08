using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace BookServiceQA
{
    [Binding]
    public class NewBookSteps
    {
        public IWebDriver driver;
        int bookCount;
        string newAuthor;
        string newTitle;

        [Given]
        public void Given_I_have_entered_the_following_values_on_the_Add_Book_form(Table table)
        {
            newAuthor = table.Rows[0]["Author"];
            newTitle = table.Rows[0]["Title"];




        /*    new SelectElement(driver.FindElement(By.CssSelector("select"))).SelectByText("Charles Dickens");
            driver.FindElement(By.Id("inputTitle")).Clear();
            driver.FindElement(By.Id("inputTitle")).SendKeys("Lorenzo");
            driver.FindElement(By.Id("inputYear")).Clear();
            driver.FindElement(By.Id("inputYear")).SendKeys("2000");
            driver.FindElement(By.Id("inputGenre")).Clear();
            driver.FindElement(By.Id("inputGenre")).SendKeys("Novel");
            driver.FindElement(By.Id("inputPrice")).Clear();
            driver.FindElement(By.Id("inputPrice")).SendKeys("10");
            driver.FindElement(By.CssSelector("form.form-horizontal > button.btn.btn-default")).Click();*/






            //driver.FindElement(By.TagName("select")).FindElement(By.LinkText(table.Rows[0]["Author"])).Click();
            driver.FindElement(By.Id("inputTitle")).SendKeys(table.Rows[0]["Title"]);
            driver.FindElement(By.Id("inputYear")).SendKeys(table.Rows[0]["Year"]);
            driver.FindElement(By.Id("inputGenre")).SendKeys(table.Rows[0]["Genre"]);
            driver.FindElement(By.Id("inputPrice")).SendKeys(table.Rows[0]["Price"]);
        }
        
        [Given]
        public void Given_I_have_entered_vAuthor_vTitle_vYear_vGenre_vPrice()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When]
        public void When_I_press_Submit()
        {
            bookCount = driver.FindElement(By.TagName("ul")).FindElements(By.TagName("li")).Count;
            driver.FindElement(By.LinkText("Submit"));
        }
        
        [Then]
        public void Then_the_new_book_is_displayed_in_the_book_list()
        {
            Assert.That(driver.FindElement(By.TagName("ul")).FindElements(By.TagName("li"))[bookCount].FindElements(By.TagName("span"))[0], Is.EqualTo(newAuthor));
            Assert.That(driver.FindElement(By.TagName("ul")).FindElements(By.TagName("li"))[bookCount].FindElements(By.TagName("span"))[1], Is.EqualTo(newTitle));
        }
        
        [Then]
        public void Then_I_will_not_be_able_to_add_the_book()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then]
        public void Then_an_error_message_will_be_displayed()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
