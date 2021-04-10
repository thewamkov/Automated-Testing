using Atata;

namespace Aatata.PageObjects
{
    using _ = ProductListPageObject;
    
    public class ProductListPageObject : Page<_>
    {
        [FindByXPath("(//span[contains(@class, 'goods-tile__price-value')])[1]")]
        public Text<_> Price { get; set; }

        [FindByXPath("(//a[contains(@class, 'goods-tile__heading')])[1]")]
        public Link<ProductPageObject, _> ProductPage { get; set; }

        
    }
}