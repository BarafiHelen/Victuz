using MvvmHelpers;
using System.Windows.Input;
using Microsoft.Maui.Media;
using Microsoft.Maui.Storage;

namespace Victuz.ViewModels;

public class ProfileViewModel : BaseViewModel
{
    public static ProfileViewModel Instance { get; } = new ProfileViewModel();

    private string _name;
    private string _email;
    private string _phoneNumber;
    private string _profilePhoto;

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    public string PhoneNumber
    {
        get => _phoneNumber;
        set => SetProperty(ref _phoneNumber, value);
    }

    public string ProfilePhoto
    {
        get => _profilePhoto;
        set => SetProperty(ref _profilePhoto, value);
    }

    public ICommand SaveCommand { get; }
    public ICommand PickPhotoCommand { get; }
    public ICommand TakePhotoCommand { get; }

    private ProfileViewModel()
    {
        // Simuleer ingelogde gebruiker
        Name = "helen barafi";
        Email = "helen@example.com";
        PhoneNumber = "123";

        SaveCommand = new Command(SaveProfile);
        PickPhotoCommand = new Command(async () => await PickPhoto());
        TakePhotoCommand = new Command(async () => await TakePhoto());
    }

    private async void SaveProfile()
    {
        // Valideer gegevens
        if (string.IsNullOrWhiteSpace(Name))
        {
            await App.Current.MainPage.DisplayAlert("Error", "Naam is verplicht.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(PhoneNumber))
        {
            await App.Current.MainPage.DisplayAlert("Error", "Telefoonnummer is verplicht.", "OK");
            return;
        }

        // Sla wijzigingen op (bijvoorbeeld in de database)
        await App.Current.MainPage.DisplayAlert("Succes", "Profiel succesvol bijgewerkt!", "OK");
    }

    private async Task PickPhoto()
    {
        var result = await MediaPicker.PickPhotoAsync();
        if (result != null)
        {
            ProfilePhoto = result.FullPath;
        }
    }

    private async Task TakePhoto()
    {
        var result = await MediaPicker.CapturePhotoAsync();
        if (result != null)
        {
            var newFile = Path.Combine(FileSystem.AppDataDirectory, "profile_photo.jpg");
            using var stream = await result.OpenReadAsync();
            using var newStream = File.OpenWrite(newFile);
            await stream.CopyToAsync(newStream);
            ProfilePhoto = newFile;
        }
    }
}
