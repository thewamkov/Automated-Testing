using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.PageObjects;

namespace SeleniumTests
{
   

    public class Tests
    {
        private IWebDriver driver;
        private int price = 0;
        
        
            
       

        [SetUp]
        public void Setup()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Navigate().GoToUrl("https://rozetka.com.ua");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();

        }


        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
        
        
        [Test]
        // [Repeat(20)]
        public void PriceCheck_TileAndProdPagePriceShouldBeEqual_ReturnsTrue()
        {
            // Act
            var mainMenu = new  MainMenuPageObject(driver); 
            

            // Arrange
            mainMenu.ChangeLanguage();
            mainMenu.SearchFor("lenovo");
            price = mainMenu.getTilePrice();

            // Assert
            Assert.AreEqual(price, mainMenu.OpenProductPage().ProductPagePrice());
        }

        
        // [Test]
        // public void Test2()
        // {
        //     // Act
        //     var mainMenu = new  MainMenuPageObject(driver);
        //     
        //     // Arrange
        //     driver.Navigate().Back();
        //     var backPrice = mainMenu.getTilePrice();
        //     // Assert
        //     Assert.AreEqual(price, backPrice);
        //     
        // }
    }
}