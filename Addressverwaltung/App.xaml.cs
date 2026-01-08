using Adressverwaltung.Views;

namespace Adressverwaltung;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new NavigationPage(new AddressListPage());
    }
}
