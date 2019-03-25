using System.IO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
//using Task2.Pages;
using Task2.Utils;

namespace Task2.Tests
{
    [TestFixture]
    public class SearchTest
    {
        [OneTimeSetUp]
        public void SetUpEverything()
        {
            Singleton.GetInstance().Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            Singleton.GetInstance().Manage().Window.Maximize();
            //Singleton.GetInstance().Navigate().GoToUrl(TestData.baseUrl);
        }

        
        public static List<Cars> list = Cars.DeserializeCar();
        [Test, TestCaseSource("list")]
        public void TestSearch(Cars car)
        {
            Singleton.GetInstance().Navigate().GoToUrl(TestData.baseUrl);

            
            //MainPage.GoToTheSearchPage();
            SearchPage.StartSearch(car.Manufacture, car.Model, car.MinYear, car.MaxYear);
            CsvWriter.WriteInfoToCsv(SearchPage.GetSearchResults());
            //Files.WriteResultToFile(SearchPage.GetSearchResults());
            //Assert.True(SearchPage.GetSearchResults());
        }

        [OneTimeTearDown]
        public void CleanAll()
        {
            Singleton.GetInstance().Quit();
        }
    }
}
