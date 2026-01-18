using Adressverwaltung.Models;
using Adressverwaltung.ViewModels;

namespace Adressverwaltung.Views;

public partial class AddressEditPicturePage : ContentPage
{
    public AddressEditPicturePage(Address address = null)
    {
        InitializeComponent();

        // ViewModel mit optionalem Address-Objekt
        BindingContext = new AddressEditPictureViewModel(address);
    }
}

