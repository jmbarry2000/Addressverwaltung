using Adressverwaltung.Models;
using Adressverwaltung.ViewModels;

namespace Adressverwaltung.Views;

public partial class AddressEditPage : ContentPage
{
    public AddressEditPage()
    {
        InitializeComponent();
        BindingContext = new AddressEditViewModel();
    }

    public AddressEditPage(object address)
    {
        InitializeComponent();
        BindingContext = new AddressEditViewModel(address as Address);
    }
}
