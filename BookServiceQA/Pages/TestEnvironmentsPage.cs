using NUnit;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookServiceQA.Pages
{
    public class TestEnvironmentsPage
    {
        ReadOnlyCollection<IWebElement> Env;

        public TestEnvironmentsPage()
        {
            Env = Browser.Driver().FindElements(By.LinkText("Details"));
        }

        public void SelectEnvironment(int num)
        {
            Env[num].Click();
        }
    }
}
