using MvvmHelpers;
using System.Windows.Input;

namespace Victuz.ViewModels;

public class ProfileViewModel : BaseViewModel
{
    public static ProfileViewModel Instance { get; } = new ProfileViewModel();

    private string _name;
    private string _email;
    private string _phoneNumber;

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

    public ICommand SaveCommand { get; }

    private ProfileViewModel()
    {
        // Simuleer ingelogde gebruiker
        Name = "helen barafi";
        Email = "helen@example.com";
        PhoneNumber = "123";

        SaveCommand = new Command(SaveProfile);
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
}