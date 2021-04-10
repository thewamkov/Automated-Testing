using Atata;

namespace Aatata.PageObjects
{
    using _ = ProductPageObject;
    public class ProductPageObject : Page<_>
    {
        [FindByXPath("//p[contains(@class, 'product-prices__big')]")]
        public Text<_> ProductPagePrice { get; set; }
    }
}