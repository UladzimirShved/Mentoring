using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Net;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Interactions;
using System.Collections.Generic;

namespace Task2
{
    public class Waiters
    {
        public static IWebElement waitUntilElementVisible(string locator)
        {
            int waitingtime = Int32.Parse(TestData.waitingTime);
            IWebElement tmp = Singleton.GetInstance().FindElement(By.XPath(locator));
            WebDriverWait wait = new WebDriverWait(Singleton.GetInstance(), TimeSpan.FromSeconds(waitingtime));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
            return tmp;
        }
        public static IReadOnlyCollection<IWebElement> waitUntilElementsVisible(string locator)
        {
            int waitingtime = Int32.Parse(TestData.waitingTime);
            var tmp = Singleton.GetInstance().FindElements(By.XPath(locator));
            WebDriverWait wait = new WebDriverWait(Singleton.GetInstance(), TimeSpan.FromSeconds(waitingtime));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
            return tmp;
        }

        public static IWebElement waitUntilElementInvisible(string locator)
        {
            int waitingtime = Int32.Parse(TestData.waitingTime);
            IWebElement tmp = Singleton.GetInstance().FindElement(By.XPath(locator));
            WebDriverWait wait = new WebDriverWait(Singleton.GetInstance(), TimeSpan.FromSeconds(waitingtime));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(locator)));
            return tmp;
        }
        public static IWebElement waitStalenessOfElement(string locator)
        {
            int waitingtime = Int32.Parse(TestData.waitingTime);
            
            WebDriverWait wait = new WebDriverWait(Singleton.GetInstance(), TimeSpan.FromSeconds(waitingtime));
            wait.Until(ExpectedConditions.StalenessOf(Singleton.GetInstance().FindElement(By.XPath(locator))));
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath(locator)));
            IWebElement tmp = Singleton.GetInstance().FindElement(By.XPath(locator));
            return tmp;
        }
    }
}
