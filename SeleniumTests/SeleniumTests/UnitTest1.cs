using System;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumTests.PageObjects;


namespace SeleniumTests
{
    public class Tests
    {
        private IWebDriver _driver;


        [SetUp]
        public void Setup()
        {
            // var chromeOptions = new ChromeOptions();
            // chromeOptions.AddArgument("headless");
            _driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            _driver.Navigate().GoToUrl("https://rozetka.com.ua");
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            // _driver.Manage().Cookies.DeleteAllCookies();
            _driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }


        

        [TestCase("Lenovo Legion")]
        [Repeat(20)]
        public void PriceCheck_TileAndProdPagePriceShouldBeEqual_ReturnsTrue( string value)
        {
            // Act
            var mainMenu = new MainMenuPageObject(_driver);

            
            // Arrange
            var prodList = mainMenu.OpenMenu()
                .ChangeLanguage()
                .Type(value)
                .Search();

            var price = prodList
                .GetTilePrice();

            var prodPage = prodList.OpenProductPage();
            
            var pagePrice = prodPage.ProductPagePrice();

            var newTilePrice = prodPage.GoBack()
                .GetTilePrice();


            // Assert
            Assert.AreEqual(price, pagePrice);
            Assert.AreEqual(price, newTilePrice);
        }
    }
}