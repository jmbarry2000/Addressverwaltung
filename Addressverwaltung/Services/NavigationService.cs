using Adressverwaltung.Views;

namespace Adressverwaltung.Services;

public class NavigationService
{
    public async Task GoToAddAsync()
    {
        await Application.Current.MainPage.Navigation
            .PushAsync(new AddressEditPage());
    }

    public async Task GoToEditAsync(object parameter)
    {
        await Application.Current.MainPage.Navigation
            .PushAsync(new AddressEditPage(parameter));
    }

    public async Task GoBackAsync()
    {
        await Application.Current.MainPage.Navigation.PopAsync();
    }
}
