using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Adressverwaltung.Models;
using System.Collections.ObjectModel;

namespace Adressverwaltung.ViewModels;

public partial class AddressListViewModel : ObservableObject
{
    [ObservableProperty]
    ObservableCollection<Address> addresses;

    [ObservableProperty]
    string searchText;

    public AddressListViewModel()
    {
        LoadAddresses();
    }

    // Adressen aus der DB laden
    async void LoadAddresses()
    {
        var list = await MauiProgram.Database.GetAddressesAsync();
        Addresses = new ObservableCollection<Address>(list);
    }

    // Adresse löschen
    [RelayCommand]
    async Task Delete(Address address)
    {
        await MauiProgram.Database.DeleteAddressAsync(address);
        LoadAddresses();
    }

    // neue Adresse hinzufügen
    [RelayCommand]
    async Task Add()
    {
        await Application.Current.MainPage.Navigation
            .PushAsync(new Views.AddressEditPage());
    }

    // vorhandene Adresse bearbiten
    [RelayCommand]
    async Task Edit(Address address)
    {
        if (address == null) return;

        await Application.Current.MainPage.Navigation
            .PushAsync(new Views.AddressEditPage(address));
    }

    // Suche / Filter
    partial void OnSearchTextChanged(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            LoadAddresses();
            return;
        }

        var filtered = Addresses.Where(a =>
            (a.FirstName ?? "").Contains(value, StringComparison.OrdinalIgnoreCase) ||
            (a.LastName ?? "").Contains(value, StringComparison.OrdinalIgnoreCase) ||
            (a.City ?? "").Contains(value, StringComparison.OrdinalIgnoreCase) ||
            (a.ZipCode ?? "").Contains(value) ||
            (a.Company ?? "").Contains(value, StringComparison.OrdinalIgnoreCase)
        );

        Addresses = new ObservableCollection<Address>(filtered);
    }
}
