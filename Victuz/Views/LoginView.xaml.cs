using Microsoft.Win32;
using System.Security.Cryptography;
using System.Text;
using Victuz.Models;
using Victuz.Views;

namespace Victuz.Views;

public partial class LoginView : ContentPage
{
    public LoginView()
    {
        InitializeComponent();
    }
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        // Controleer of de velden zijn ingevuld
        if (string.IsNullOrEmpty(EmailEntry.Text) || string.IsNullOrEmpty(PasswordEntry.Text))
        {
            await DisplayAlert("Error", "Please fill in all fields", "OK");
            return;
        }

        // Zoek naar een gebruiker met overeenkomend e-mailadres en wachtwoord
        var user = (await App.Database.GetItemsAsync<User>())
            .FirstOrDefault(u => u.EmailAddress == EmailEntry.Text && u.Password == PasswordEntry.Text);

        if (user != null)
        {
            await Navigation.PushAsync(new EventView());
        }
        else
        {
            await DisplayAlert("Error", "Invalid email or password", "OK");
        }
    }
    
    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterView());
    }
}