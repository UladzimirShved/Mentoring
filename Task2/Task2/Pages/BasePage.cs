using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Pages
{
    public class BasePage
    {
        public IWebDriver Driver { get; set; }

        public BasePage()
        {
            Driver = Singleton.GetInstance();
        }

        public void GoTo(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        public void Quit()
        {
            Driver.Quit();
        }
    }
}

