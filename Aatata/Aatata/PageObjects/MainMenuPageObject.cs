using Atata;

namespace Aatata.PageObjects
{
    using _ = MainMenuPageObject;
    [Url(" ")]
    public class MainMenuPageObject : Page<_>
    {
        [FindByXPath("//button[contains(@aria-label, 'Откр')]")]
        public Button<_> SideBar { get; set; }
        
        [FindByXPath("(//a[contains(text(), 'UA')])[2]")]
        public Text<_> RuLanguage { get; set; }
                      
        [FindByXPath("//input[contains(@placeholder, 'Я шукаю')]")]
        public TextInput<_> RuSearch { get; set; }

        [FindByXPath("//button[contains(text(), 'Знайти')]")]
        public Button<ProductListPageObject, _> SearchButton { get; set; }
    }
}