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
    static BaseElement manufactureList = new BaseElement(By.XPath("//select[@class='manufacture']"), "Manufacture list");
    static BaseElement modelList = new BaseElement(By.XPath("//select[@class='model']"), "Model list");
    static BaseElement minYearList = new BaseElement(By.XPath("//select[@name='min-year']"), "Min Year List");
    static BaseElement maxYearList = new BaseElement(By.XPath("//select[@name='max-year']"), "Max Year list");

    static BaseElement fastNavigationPages = new BaseElement(By.XPath("//td[@class='pagination']//li"), "Fast Navigation Pages");
    static BaseElement nextPage = new BaseElement(By.XPath("//img[@src='/content/img/page-next.gif']"), "Next Page");

    static BaseElement tmpPageCars = new BaseElement(By.XPath("//tbody"), "Full Table");
    static BaseElement tmpCar = new BaseElement(By.XPath("//tr[@class='carRow']"), "Tmp Car");

    public static void StartSearch(string manufacture, string model, int minYear, int maxYear)
    {
      string manufactureXpath = $"//option[contains(text(),'{manufacture.ToString()}')]";
      string modelXpath = $"//option[contains(text(),'{model.ToString()}')]";
      string minYearXpath = $"//select[@name='min-year']//option[contains(text(),'{minYear.ToString()}')]";
      string maxYearXpath = $"//select[@name='max-year']//option[contains(text(),'{maxYear.ToString()}')]";

      BaseElement tmpManufacture = new BaseElement(By.XPath(manufactureXpath), "Tmp Manufacture");
      BaseElement tmpModel = new BaseElement(By.XPath(modelXpath), "Tmp Model");
      BaseElement tmpMinYear = new BaseElement(By.XPath(minYearXpath), "Tmp MinYear");
      BaseElement tmpMaxYear = new BaseElement(By.XPath(maxYearXpath), "Tmp MaxYear");

      manufactureList.MyClick();
      tmpManufacture.MyClick();

      modelList.MyClick();
      tmpModel.MyClick();

      minYearList.MyClick();
      tmpMinYear.MyClick();


      maxYearList.MyClick();
      tmpMaxYear.MyClick();
      maxYearList.MyClick();

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

      if (Singleton.GetInstance().FindElements(fastNavigationPages.locator).Count != 0)
      {
        while (nextPage.GetElement() != null)
        {
          Thread.Sleep(3000);
          var tmpPageCarsList = Singleton.GetInstance().FindElements(tmpCar.locator);
          foreach (var car in tmpPageCarsList)
          {
            tmpCarName = car.FindElements(By.XPath(".//a//strong")).ToList().First().Text.ToString();
            tmpCarLink = car.FindElements(By.XPath(".//a")).ToList().First().GetAttribute("href").ToString();
            tmpCarCost = car.FindElement(By.XPath(".//p[contains(text(), '$')]")).Text.ToString();
            tmpPos = tmpCarCost.LastIndexOf('$');
            tmpCarCost = tmpCarCost.Substring(0, tmpPos + 1);
            tmpCarYear = car.FindElement(By.XPath(".//span[@class='year']")).Text.ToString();
            tmpCarInfo = "model: " + tmpCarName + " href: " + tmpCarLink + " cost: " + tmpCarCost + " year: " + tmpCarYear;
            carsInfo.Add(tmpCarInfo);
          }
          try
          {
            nextPage.MoveToElement();
            nextPage.MyClick();
          }
          catch (NoSuchElementException e)
          {
            break;
          }
        }
      }
      else
      {
        var tmpPageCarsList = Singleton.GetInstance().FindElements(tmpCar.locator);
        foreach (var car in tmpPageCarsList)
        {
          tmpCarName = car.FindElements(By.XPath(".//a//strong")).ToList().First().Text.ToString();
          tmpCarLink = car.FindElements(By.XPath(".//a")).ToList().First().GetAttribute("href").ToString();
          tmpCarCost = car.FindElement(By.XPath(".//p[contains(text(), '$')]")).Text.ToString();
          tmpPos = tmpCarCost.LastIndexOf('$');
          tmpCarCost = tmpCarCost.Substring(0, tmpPos + 1);
          tmpCarYear = car.FindElement(By.XPath(".//span[@class='year']")).Text.ToString();
          tmpCarInfo = "model: " + tmpCarName + " href: " + tmpCarLink + " cost: " + tmpCarCost + " year: " + tmpCarYear;
          carsInfo.Add(tmpCarInfo);
        }
      }

      return carsInfo;
    }

  }

}
