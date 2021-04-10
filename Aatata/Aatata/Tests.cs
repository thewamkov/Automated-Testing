using System;
using Aatata.PageObjects;
using Atata;
using NUnit.Framework;

namespace Aatata
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            AtataContext.GlobalConfiguration
                .UseChrome()
                .WithArguments("start-maximized")
                .UseBaseUrl("https://rozetka.com.ua")
                .Build();
        }
        [TearDown]
        public void TearDown()
        {
            AtataContext.Current?.CleanUp();
        }
        
        [Test]
        [Repeat(20)]
        public void Test1()
        {
            //Arrange
            string TilePrice;
            string ProductPagePrice;
            string AgainTilePrice;
            
            //Act
            Go.To<MainMenuPageObject>()
                .SideBar.Click()
                .RuLanguage.Click()
                .RuSearch.Type("Lenovo Legion")
                .SearchButton.ClickAndGo()
                .Price.Attributes.Value.Get(out TilePrice)
                .ProductPage.ClickAndGo()
                .ProductPagePrice.Attributes.Value.Get(out ProductPagePrice)
                .GoBack<ProductListPageObject>()
                .Price.Attributes.Value.Get(out AgainTilePrice);
            
            //Assert
            Assert.AreEqual(Convert.ToInt16(TilePrice), Convert.ToInt16(ProductPagePrice), Convert.ToInt16(AgainTilePrice));
        }
    }
}