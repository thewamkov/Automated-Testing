using System;
using System.Text.RegularExpressions;
using OpenQA.Selenium;

namespace SeleniumTests.PageObjects
{
    
    public class ProductsListPageObject
    {
        private readonly IWebDriver _webDriver;
        private readonly By _tilePrice = By.XPath("(//span[contains(@class, 'goods-tile__price-value')])[1]");
        private readonly By _productPage = By.XPath("(//span[contains(@class, 'goods-tile__title')])[1]");
        
        
        public  ProductsListPageObject( IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }
        
        
        /// <summary>
        /// This function Extract Price of product from tile .
        /// </summary>
        /// <returns>Returns price of product from tile of products .</returns>
        public int GetTilePrice()
        { 
            var tilePrice = Convert.ToInt32(Regex.Replace(_webDriver.FindElement(_tilePrice).Text, @"\s+", ""));

            return tilePrice;
        }
        
        
        /// <summary>
        /// This function navigates to product page and returns ProductPageObject object .
        /// </summary>
        /// <returns>Object of class ProductsListPageObject for accessing it's methods .</returns>
        public ProductPageObject OpenProductPage()
        {
            _webDriver.FindElement(_productPage).Click();

            return new ProductPageObject(_webDriver);
        }
        
    }
}