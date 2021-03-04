using System;
using System.Linq;
using System.Text.RegularExpressions;
using OpenQA.Selenium;

namespace SeleniumTests.PageObjects
{
    public class ProductPageObject
    {
        private readonly IWebDriver _webDriver;
        private readonly By _productPagePrice = By.XPath("//p[contains(@class, 'product-prices__big')]");

        public ProductPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        
        /// <summary>
        /// This function Extract Price of product from  product page .
        /// </summary>
        /// <returns>Returns price of product from tile of products .</returns>
        public int ProductPagePrice()
        {
            var txtPrice = string.Concat(_webDriver.FindElement(_productPagePrice).Text.Where(char.IsDigit));
            var tilePrice = Convert.ToInt32(Regex.Replace(txtPrice, @"\s+", ""));

            return tilePrice;
        }
        
        /// <summary>
        /// This function makes step back in browser .
        /// </summary>
        /// <returns> It returns ProductsListPageObject object .</returns>
        public ProductsListPageObject GoBack()
        {
            _webDriver.Navigate().Back();
            return new ProductsListPageObject(_webDriver);
        }
        
    }
}