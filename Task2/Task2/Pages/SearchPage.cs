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
using Task2.Pages;

namespace Task2
{
    class SearchPage : BasePage
    {
        public SearchPage() : base() { }

        string manufactureListLocator = "//select[@class='manufacture']";
        string modelListLocator = "//select[@class='model']";
        string minYearListLocator = "//select[@name='min-year']";
        string maxYearListLocator = "//select[@name='max-year']";

        string fastNavigationPagesLocator = "//td[@class='pagination']//li";
        string nextPageLocator = "//img[@src='/content/img/page-next.gif']";

        string tmpCarLocator = "//tr[@class='carRow']";

        string tmpCarNameLocator = ".//a//strong";
        string tmpCarLinkLocator = ".//a";
        string tmpCarCostLocator = ".//p[contains(text(), '$')]";
        string tmpCarYearLocator = ".//span[@class='year']";
        string tmpCarBigInfoLocator = ".//td[(@class='txt')]//p";

        string costSorterButtonLocator = "//a[(@data='price')]";
        string costAscSorterLocator = "//a[(@data='price')and(@class='sort-up')]";
        string costDescSorterLocator = "//a[(@data='price')and(@class='sort-bt')]";

        string adsCarNameLocator = "//span[@class='autoba-fastchars-ttl']//strong";
        string adsCarYearLocator = "//span[@class='year']//strong";
        string adsCarCostLocator = "//span[@class='other-costs']//span[contains(text(), '$')]";


        string tmpCarName = "";
        string tmpCarLink = "";
        string tmpCarCost = "";
        string tmpCarYear = "";
        string tmpCarInfo = "";
        int tmpPos = 0;



        List<string> carsInfo = new List<string>();
        Dictionary<string, string> etalon = new Dictionary<string, string>();

        IWebElement carFromPage;
        IReadOnlyCollection<IWebElement> tmpPageCarsList;

        public void StartSearch(string manufacture, string model, int minYear, int maxYear)
        {
            string manufactureXpathLocator = $"//option[contains(text(),'{manufacture.ToString()}')]";
            string modelXpathLocator = $"//option[contains(text(),'{model.ToString()}')]";
            string minYearXpathLocator = $"//select[@name='min-year']//option[contains(text(),'{minYear.ToString()}')]";
            string maxYearXpathLocator = $"//select[@name='max-year']//option[contains(text(),'{maxYear.ToString()}')]";

            Waiters.waitUntilElementVisible(Driver, manufactureListLocator).Click();
            Driver.FindElement(By.XPath(manufactureXpathLocator)).Click();

            Driver.FindElement(By.XPath(modelListLocator)).Click();
            Driver.FindElement(By.XPath(modelXpathLocator)).Click();

            Driver.FindElement(By.XPath(minYearListLocator)).Click();
            Driver.FindElement(By.XPath(minYearXpathLocator)).Click();

            Driver.FindElement(By.XPath(maxYearListLocator)).Click();
            Driver.FindElement(By.XPath(maxYearXpathLocator)).Click();

        }

        public List<string> GetSearchResults()
        {
            if (Driver.FindElements(By.XPath(fastNavigationPagesLocator)).Count != 0)
            {
                while (true)
                {
                    GetCarsInfo();

                    try
                    {
                        Driver.FindElement(By.XPath(nextPageLocator)).Click();
                    }
                    catch (NoSuchElementException e)
                    {
                        break;
                    }
                }
            }
            else
            {
                GetCarsInfo();
            }

            return carsInfo;
        }

        public void GetCarsInfo()
        {
            
            tmpPageCarsList = GetTmpPageCarsList();
            foreach (var car in tmpPageCarsList)
            {
                tmpCarName = car.FindElements(By.XPath(tmpCarNameLocator)).ToList().First().Text.ToString();
                tmpCarLink = car.FindElements(By.XPath(tmpCarLinkLocator)).ToList().First().GetAttribute("href").ToString();
                tmpCarCost = GetCarPrice(car).ToString();
                tmpCarYear = car.FindElement(By.XPath(tmpCarYearLocator)).Text.ToString();
                tmpCarInfo = "model: " + tmpCarName + " href: " + tmpCarLink + " cost: " + tmpCarCost + "$ year: " + tmpCarYear;
                carsInfo.Add(tmpCarInfo);
            }
        }


        public void StartSearchByMinYear(int minYear)
        {
            string minYearXpathLocator = $"//select[@name='min-year']//option[contains(text(),'{minYear.ToString()}')]";
            Waiters.waitUntilElementVisible(Driver, minYearListLocator).Click();
            Driver.FindElement(By.XPath(minYearXpathLocator)).Click();
        }



        public void CheckAllCarsYear(int carMinYear)
        {
            if (Driver.FindElements(By.XPath(fastNavigationPagesLocator)).Count != 0)
            {
                while (true)
                {
                    CheckCarsYear(carMinYear);

                    try
                    {
                        Driver.FindElement(By.XPath(nextPageLocator)).Click();
                    }
                    catch (NoSuchElementException e)
                    {
                        break;
                    }
                }
            }
            else
            {
                CheckCarsYear(carMinYear);
            }

        }

        public void CheckCarsYear(int carMinYear)
        {
            tmpPageCarsList = GetTmpPageCarsList();
            foreach (var car in tmpPageCarsList)
            {
                tmpCarYear = car.FindElement(By.XPath(tmpCarYearLocator)).Text.ToString();
                Assert.True(Convert.ToInt32(tmpCarYear) >= carMinYear, $"tmpCar year is: {tmpCarYear.ToString()}. Min car year: {carMinYear.ToString()}");
            }
        }


        public int GetCarPrice(IWebElement car)
        {
            tmpCarCost = car.FindElement(By.XPath(tmpCarCostLocator)).Text.ToString();
            tmpPos = tmpCarCost.LastIndexOf('$');
            tmpCarCost = tmpCarCost.Substring(0, tmpPos);
            return Convert.ToInt32(tmpCarCost.Replace(" ", string.Empty));
        }

        public void SortCarsByPrice()
        {
            Waiters.waitUntilElementVisible(Driver, costSorterButtonLocator).Click();
        }


        public void CheckAllPrices()
        {
            int prevCost = -1;
            int tmpCost = -1;

            tmpPageCarsList = GetTmpPageCarsList();

            if (Driver.FindElements(By.XPath(costAscSorterLocator)).Count != 0)
            {
                //min -> max
                foreach (var car in tmpPageCarsList)
                {
                    tmpCost = GetCarPrice(car);
                    Assert.True(Convert.ToInt32(tmpCost) >= prevCost, $"tmpCar cost: {tmpCost.ToString()}. Previous car cost: {prevCost.ToString()}");
                    prevCost = tmpCost;
                }
            }

            else if (Driver.FindElements(By.XPath(costDescSorterLocator)).Count != 0)
            {
                //max -> min
                prevCost = int.MaxValue;
                foreach (var car in tmpPageCarsList)
                {
                    tmpCost = GetCarPrice(car);
                    Assert.True(Convert.ToInt32(tmpCost) <= prevCost, $"tmpCar cost: {tmpCost.ToString()}. Previous car cost: {prevCost.ToString()}");
                    prevCost = tmpCost;
                }
            }
        }

        public IReadOnlyCollection<IWebElement> GetTmpPageCarsList()
        {            
            carFromPage = Waiters.waitStalenessOfElement(Driver, tmpCarLocator);
            var tmpList = Driver.FindElements(By.XPath(tmpCarLocator));
            return tmpList;
        }

        public IWebElement GetRandomCar()
        {
            var random = new Random();
            var tmpPageCarsList = Driver.FindElements(By.XPath(tmpCarLocator));
            int index = random.Next(tmpPageCarsList.Count);
            var car = tmpPageCarsList.ToArray()[index];
            return car;
        }

        public Dictionary<string, string> GetCarInfoFromList(IWebElement car)
        {
            Dictionary<string, string> carInfoFromList = new Dictionary<string, string>();
            tmpCarName = car.FindElements(By.XPath(tmpCarNameLocator)).ToList().First().Text.ToString();
            tmpCarCost = GetCarPrice(car).ToString();
            tmpCarYear = car.FindElement(By.XPath(tmpCarYearLocator)).Text.ToString();

            carInfoFromList.Add("carName", tmpCarName);
            carInfoFromList.Add("carCost", tmpCarCost);
            carInfoFromList.Add("carYear", tmpCarYear);

            return carInfoFromList;
        }

        public void GoIntoCarAds()
        {
            var car = GetRandomCar();
            etalon = GetCarInfoFromList(car);
            car.FindElements(By.XPath(tmpCarNameLocator)).ToList().First().Click();
        }

        public bool CheckInfoInAds()
        {
            bool compare = false;
            string tmpName = Driver.FindElement(By.XPath(adsCarNameLocator)).Text.ToString();
            string tmpCost = Driver.FindElement(By.XPath(adsCarCostLocator)).Text.ToString().Replace(" ", string.Empty).Replace("$", string.Empty);
            string tmpYear = Driver.FindElement(By.XPath(adsCarYearLocator)).Text.ToString();

            if ((tmpName == etalon["carName"])&&(tmpCost == etalon["carCost"])&&(tmpYear == etalon["carYear"]))
            {
                compare = true;
            }
            return compare;
        }

    }

}
