using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BookServiceQA
{
    [SetUpFixture]
    public class AssemblySetupFixture
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Browser.Driver();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Browser.Driver().Quit();
        }
    }
}
