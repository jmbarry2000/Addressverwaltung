using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Adressverwaltung.Models;
using Adressverwaltung.Services;

namespace Adressverwaltung.ViewModels;

public partial class AddressEditViewModel : ObservableObject
{
    private readonly NavigationService _navigationService = new();
    private Address _address;

    [ObservableProperty] string firstName;
    [ObservableProperty] string lastName;
    [ObservableProperty] string company;
    [ObservableProperty] string street;
    [ObservableProperty] string zipCode;
    [ObservableProperty] string city;
    [ObservableProperty] string phone;
    [ObservableProperty] string photoPath;

    public AddressEditViewModel()
    {
        _address = new Address();
    }

    public AddressEditViewModel(Address address)
    {
        _address = address;

        FirstName = address.FirstName;
        LastName = address.LastName;
        Company = address.Company;
        Street = address.Street;
        ZipCode = address.ZipCode;
        City = address.City;
        Phone = address.Phone;
        PhotoPath = address.PhotoPath;
    }

    [RelayCommand]
    async Task Save()
    {
        _address.FirstName = FirstName;
        _address.LastName = LastName;
        _address.Company = Company;
        _address.Street = Street;
        _address.ZipCode = ZipCode;
        _address.City = City;
        _address.Phone = Phone;
        _address.PhotoPath = PhotoPath;

        await MauiProgram.Database.SaveAddressAsync(_address);
        await _navigationService.GoBackAsync();
    }

    [RelayCommand]
    async Task TakePhoto()
    {
        var photo = await MediaPicker.CapturePhotoAsync();
        if (photo == null) return;

        var newPath = Path.Combine(
            FileSystem.AppDataDirectory,
            photo.FileName);

        using var stream = await photo.OpenReadAsync();
        using var newStream = File.OpenWrite(newPath);
        await stream.CopyToAsync(newStream);

        PhotoPath = newPath;
    }

    [RelayCommand]
    async Task OpenMaps()
    {
        string address = $"{Street} {ZipCode} {City}";
        string url =
            $"https://www.google.com/maps/search/?api=1&query={Uri.EscapeDataString(address)}";

        await Launcher.OpenAsync(url);
    }
}
