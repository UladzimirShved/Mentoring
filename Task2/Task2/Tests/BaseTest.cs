using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Task2.Tests
{
    [TestFixture]
    public class BaseTest
    {
        protected dynamic page;

        [SetUp]
        public void SetUpEverything()
        {
            page = new SearchPage();
            page.GoTo(TestData.baseUrl);            
        }

        [OneTimeTearDown]
        protected void TearDown()
        {
            page.Quit();
        }
    }
}
