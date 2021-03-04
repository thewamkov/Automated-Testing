using OpenQA.Selenium;

namespace SeleniumTests.PageObjects
{
    public class MainMenuPageObject
    {
        private readonly IWebDriver _webDriver;
        private readonly By _sideMenuButton = By.XPath("//button[contains(@aria-label, 'Відкрити')]");
        private readonly By _ruLanguage = By.XPath("//a[contains(text(), 'RU')]");
        private readonly By _searchRu = By.XPath("//input[contains(@placeholder, 'Я ищу')]");
        private readonly By _searchButtonRu = By.XPath("//button[contains(text(), 'Найти')]");
        


        // Constructor
        public MainMenuPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }


        /// <summary>
        /// This function opens to the side menu by clicking burger button .
        /// </summary>
        public MainMenuPageObject OpenMenu()
        {
            _webDriver.FindElement(_sideMenuButton).Click();
            return this;
        }

        /// <summary>
        /// This function press RU(Russian) button .
        /// </summary>
        /// <returns></returns>
        public MainMenuPageObject ChangeLanguage()
        {
            _webDriver.FindElement(_ruLanguage).Click();
            return this;
        }


        /// <summary>
        /// This function types specified string(products) .
        /// </summary>
        /// <param name="item">String which will be pasted in search field.</param>
        public MainMenuPageObject Type(string item)
        {
            _webDriver.FindElement(_searchRu).SendKeys(item);
            return this;
        }

        
        /// <summary>
        /// This function press search button .
        /// </summary>
        /// <returns>Self</returns>
        public ProductsListPageObject Search()
        {
            _webDriver.FindElement(_searchButtonRu).Click();
            return new ProductsListPageObject(_webDriver);
        }
        
    }
}