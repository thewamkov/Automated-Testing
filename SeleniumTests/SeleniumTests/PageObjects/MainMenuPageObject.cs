using System;
using System.Text.RegularExpressions;
using OpenQA.Selenium;

namespace SeleniumTests.PageObjects
{
    public class MainMenuPageObject
    {
        private IWebDriver _webDriver;
        private readonly By _SideMenuButton = By.XPath("//button[contains(@aria-label, 'Відкрити')]");
        private readonly By _RULanguage = By.XPath("//a[contains(text(), 'RU')]");
        private readonly By _SearchRu = By.XPath("//input[contains(@placeholder, 'Я ищу')]");
        private readonly By _SearchButtonRu = By.XPath("//button[contains(text(), 'Найти')]");
        private readonly By _TilePrice = By.XPath("(//span[contains(@class, 'goods-tile__price-value')])[1]");
        private readonly By _ProductPage = By.XPath("(//span[contains(@class, 'goods-tile__title')])[1]");
        
        
        // Constructor
        public MainMenuPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }
        
        
        /// <summary>
        /// This function press side menu burger button and after that  on left side appeared menu press RU  button which
        /// change language. The menu is closed after swapping language.
        /// </summary>
        public void ChangeLanguage()
        {
            _webDriver.FindElement(_SideMenuButton).Click();
            _webDriver.FindElement(_RULanguage).Click();
        }

        public void SearchFor( string item )
        {
            _webDriver.FindElement(_SearchRu).SendKeys(item);
            _webDriver.FindElement(_SearchButtonRu).Click();
            
        }

        public int getTilePrice()
        {
            var tilePrice = Convert.ToInt32(Regex.Replace(_webDriver.FindElement(_TilePrice).Text, @"\s+", ""));

            return tilePrice;

        }

        public ProductPageObject OpenProductPage()
        {
            _webDriver.FindElement(_ProductPage).Click();

            return new ProductPageObject(_webDriver);
        }
        
    }
}