using System;
using System.Linq;
using System.Text.RegularExpressions;
using OpenQA.Selenium;

namespace SeleniumTests.PageObjects
{
    public class ProductPageObject
    {
        private IWebDriver _webDriver;
        private readonly By _ProductPagePrice = By.XPath("//p[contains(@class, 'product-prices__big product-prices__big_color_red')]");
        
        public ProductPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public int ProductPagePrice()
        {
            var txtPrice = string.Concat(_webDriver.FindElement(_ProductPagePrice).Text.Where(char.IsDigit));
            var tilePrice = Convert.ToInt32(Regex.Replace(txtPrice, @"\s+", ""));

            return tilePrice;
        }
        
        
    }
}