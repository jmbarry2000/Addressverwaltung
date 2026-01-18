using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Adressverwaltung.Models;
using Microsoft.Maui.ApplicationModel;
using System.IO;
using System.Threading.Tasks;

namespace Adressverwaltung.ViewModels;

public partial class AddressEditPictureViewModel : ObservableObject
{
    [ObservableProperty]
    Address editedAddress;

    [ObservableProperty]
    string photoPath;

    public AddressEditPictureViewModel(Address address = null)
    {
        EditedAddress = address ?? new Address();
    }

    [RelayCommand]
    public async Task TakePhoto()
    {
        try
        {
            if (!await EnsureCameraPermissionAsync())
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Berechtigung erforderlich",
                    "Die Kamera-Berechtigung wird benötigt, um ein Foto aufzunehmen.",
                    "OK");
                return;
            }

            var photo = await MediaPicker.Default.CapturePhotoAsync();
            if (photo == null) return;

            var newFile = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
            using var stream = await photo.OpenReadAsync();
            using var newStream = File.OpenWrite(newFile);
            await stream.CopyToAsync(newStream);

            PhotoPath = newFile;
            EditedAddress.PhotoPath = newFile;
        }
        catch (FeatureNotSupportedException)
        {
            await Application.Current.MainPage.DisplayAlert(
                "Fehler",
                "Die Kamera wird auf diesem Gerät nicht unterstützt.",
                "OK");
        }
        catch (PermissionException)
        {
            await Application.Current.MainPage.DisplayAlert(
                "Fehler",
                "Die Kamera-Berechtigung wurde verweigert.",
                "OK");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert(
                "Fehler",
                $"Beim Aufnehmen des Fotos ist ein Fehler aufgetreten: {ex.Message}",
                "OK");
        }
    }

    [RelayCommand]
    public async Task Save()
    {
        if (EditedAddress.Id == 0)
            await MauiProgram.Database.SaveAddressAsync(EditedAddress);
        else
            await MauiProgram.Database.UpdateAddressAsync(EditedAddress);

        await Application.Current.MainPage.Navigation.PopAsync();
    }

    private async Task<bool> EnsureCameraPermissionAsync()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
        if (status != PermissionStatus.Granted)
        {
            status = await Permissions.RequestAsync<Permissions.Camera>();
        }
        return status == PermissionStatus.Granted;
    }
}
