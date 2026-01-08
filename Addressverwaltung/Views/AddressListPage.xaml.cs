using Adressverwaltung.ViewModels;

namespace Adressverwaltung.Views;

public partial class AddressListPage : ContentPage
{
    public AddressListPage()
    {
        InitializeComponent();

        BindingContext = new AddressListViewModel();
    }
}
