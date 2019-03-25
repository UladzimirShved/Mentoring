using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Net;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Interactions;

namespace Task2
{
    public class BaseElement
    {
        public By locator;
        public String name;
        public BaseElement(By locator, String name)
        {
            this.locator = locator;
            this.name = name;
        }
        public BaseElement(By locator) { }
        public BaseElement() { }

        public void MyClick()
        {           
            waitUntilElementVisible().Click();
        }
        public void MoveToElement()
        {
            Actions actions = new Actions(Singleton.GetInstance());
            actions.MoveToElement(waitUntilElementVisible());
            actions.Perform();
        }
        public IWebElement waitUntilElementVisible()
        {
            int waitingtime = Int32.Parse(TestData.waitingTime);
            IWebElement tmp = Singleton.GetInstance().FindElement(locator);
            WebDriverWait wait = new WebDriverWait(Singleton.GetInstance(), TimeSpan.FromSeconds(waitingtime));
            wait.Until(ExpectedConditions.ElementIsVisible(locator));
            return tmp;
        }

        public IWebElement waitUntilElementInvisible()
        {
            int waitingtime = Int32.Parse(TestData.waitingTime);
            IWebElement tmp = Singleton.GetInstance().FindElement(locator);
            WebDriverWait wait = new WebDriverWait(Singleton.GetInstance(), TimeSpan.FromSeconds(waitingtime));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
            return tmp;
        }
        public IWebElement GetElement()
        {
            try
            {
                return Singleton.GetInstance().FindElement(locator);
            }
            catch (NoSuchElementException e)
            {                
                return null;
            }
        }

        public void FillLabel(string info)
        {           
            waitUntilElementVisible().SendKeys(info);
        }


    }
}
