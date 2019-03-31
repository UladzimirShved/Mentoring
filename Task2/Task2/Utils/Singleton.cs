using System.IO;
using NUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Text;
using System.Threading.Tasks;


namespace Task2
{
    public class Singleton
    {
        private static IWebDriver instance = null;
        protected Singleton()
        {
        }
        public static IWebDriver GetInstance()
        {
            string browser = TestData.browser;
          
                if (instance == null)
                {
                    instance = BrowserFactory.GetBrowser(browser);
                }
            instance.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            instance.Manage().Window.Maximize();
            return instance;
        }
    }
}
