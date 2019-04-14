using System.IO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Task2.Utils;

namespace Task2.Tests
{
    [TestFixture]
    public class SearchTest : BaseTest
    {

        public static List<Cars> list = Cars.DeserializeCar();
        [Test, TestCaseSource("list")]
        public void TestSearch(Cars car)
        {
            page.StartSearch(car.Manufacture, car.Model, car.MinYear, car.MaxYear);
            CsvWriter.WriteInfoToCsv(page.GetSearchResults());
        }

        [Test, TestCaseSource("list")]
        public void TestSearchByMinYear(Cars car)
        {
            page.StartSearchByMinYear(car.MinYear);
            page.CheckAllCarsYear(car.MinYear);
        }

        [Test]
        public void TestSortPrice()
        {
            //не уверен, стоит ли разнести проверку сортировки ASC и DESC в разные тесты
            page.SortCarsByPrice();
            page.CheckAllPrices();

            page.SortCarsByPrice();
            page.CheckAllPrices();
        }

        [Test]
        public void TestRandomAdsInfo()
        {
            page.GoIntoCarAds();
            Assert.True(page.CheckInfoInAds(), "Info from the list of ads is not equal to info inside ads");
        }

    }
}
