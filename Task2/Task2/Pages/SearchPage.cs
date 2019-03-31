using System.IO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;

namespace Task2
{
    public class SearchPage
    {
        static IWebDriver driver = Singleton.GetInstance();

        static string manufactureListLocator = "//select[@class='manufacture']";
        static string modelListLocator = "//select[@class='model']";
        static string minYearListLocator = "//select[@name='min-year']";
        static string maxYearListLocator = "//select[@name='max-year']";

        static string fastNavigationPagesLocator = "//td[@class='pagination']//li";
        static string nextPageLocator = "//img[@src='/content/img/page-next.gif']";

        static string tmpCarLocator = "//tr[@class='carRow']";

        static string tmpCarNameLocator = ".//a//strong";
        static string tmpCarLinkLocator = ".//a";
        static string tmpCarCostLocator = ".//p[contains(text(), '$')]";
        static string tmpCarYearLocator = ".//span[@class='year']";

        static IWebElement carFromPage;
        static IReadOnlyCollection<IWebElement> tmpPageCarsList;

        public static void StartSearch(string manufacture, string model, int minYear, int maxYear)
        {
            string manufactureXpathLocator = $"//option[contains(text(),'{manufacture.ToString()}')]";
            string modelXpathLocator = $"//option[contains(text(),'{model.ToString()}')]";
            string minYearXpathLocator = $"//select[@name='min-year']//option[contains(text(),'{minYear.ToString()}')]";
            string maxYearXpathLocator = $"//select[@name='max-year']//option[contains(text(),'{maxYear.ToString()}')]";

            Waiters.waitUntilElementVisible(manufactureListLocator).Click();
            Waiters.waitUntilElementVisible(manufactureXpathLocator).Click();

            Waiters.waitUntilElementVisible(modelListLocator).Click();
            Waiters.waitUntilElementVisible(modelXpathLocator).Click();

            Waiters.waitUntilElementVisible(minYearListLocator).Click();
            Waiters.waitUntilElementVisible(minYearXpathLocator).Click();

            Waiters.waitUntilElementVisible(maxYearListLocator).Click();
            Waiters.waitUntilElementVisible(maxYearXpathLocator).Click();
            Waiters.waitUntilElementVisible(maxYearListLocator).Click();

        }

        public static List<string> GetSearchResults()
        {
            string tmpCarName = "";
            string tmpCarLink = "";
            string tmpCarCost = "";
            string tmpCarYear = "";
            string tmpCarInfo = "";
            int tmpPos = 0;
            List<string> carsInfo = new List<string>();

            if (Singleton.GetInstance().FindElements(By.XPath(fastNavigationPagesLocator)).Count != 0)
            {
                while (true)
                {
                    
                    carFromPage = Waiters.waitStalenessOfElement(tmpCarLocator);
                    tmpPageCarsList = driver.FindElements(By.XPath(tmpCarLocator));
                    foreach (var car in tmpPageCarsList)
                    {
                        tmpCarName = car.FindElements(By.XPath(tmpCarNameLocator)).ToList().First().Text.ToString();
                        tmpCarLink = car.FindElements(By.XPath(tmpCarLinkLocator)).ToList().First().GetAttribute("href").ToString();
                        tmpCarCost = car.FindElement(By.XPath(tmpCarCostLocator)).Text.ToString();
                        tmpPos = tmpCarCost.LastIndexOf('$');
                        tmpCarCost = tmpCarCost.Substring(0, tmpPos + 1);
                        tmpCarYear = car.FindElement(By.XPath(tmpCarYearLocator)).Text.ToString();
                        tmpCarInfo = "model: " + tmpCarName + " href: " + tmpCarLink + " cost: " + tmpCarCost + " year: " + tmpCarYear;
                        carsInfo.Add(tmpCarInfo);
                    }
                    try
                    {
                        driver.FindElement(By.XPath(nextPageLocator)).Click();

                    }
                    catch (NoSuchElementException e)
                    {
                        break;
                    }
                }
            }
            else
            {
                carFromPage = Waiters.waitStalenessOfElement(tmpCarLocator);
                tmpPageCarsList = driver.FindElements(By.XPath(tmpCarLocator));
                foreach (var car in tmpPageCarsList)
                {
                    tmpCarName = car.FindElements(By.XPath(tmpCarNameLocator)).ToList().First().Text.ToString();
                    tmpCarLink = car.FindElements(By.XPath(tmpCarLinkLocator)).ToList().First().GetAttribute("href").ToString();
                    tmpCarCost = car.FindElement(By.XPath(tmpCarCostLocator)).Text.ToString();
                    tmpPos = tmpCarCost.LastIndexOf('$');
                    tmpCarCost = tmpCarCost.Substring(0, tmpPos + 1);
                    tmpCarYear = car.FindElement(By.XPath(tmpCarYearLocator)).Text.ToString();
                    tmpCarInfo = "model: " + tmpCarName + " href: " + tmpCarLink + " cost: " + tmpCarCost + " year: " + tmpCarYear;
                    carsInfo.Add(tmpCarInfo);
                }
            }

            return carsInfo;
        }

    }

}
