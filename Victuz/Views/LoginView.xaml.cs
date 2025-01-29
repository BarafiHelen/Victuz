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
        if (string.IsNullOrEmpty(EmailEntry.Text) || string.IsNullOrEmpty(PasswordEntry.Text))
        {
            await DisplayAlert("Error", "Please fill in all fields", "OK");
            return;
        }

        var user = (await App.Database.GetItemsAsync<User>())
            .FirstOrDefault(u => u.EmailAddress == EmailEntry.Text && u.Password == PasswordEntry.Text);

        if (user != null)
        {
            await SecureStorage.SetAsync("LoggedInUserID", user.ID.ToString());
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

    private async void OnForgotPasswordClicked(object sender, EventArgs e)
    {
        string email = await DisplayPromptAsync("Forgot Password", "Enter your email:", "OK", "Cancel");

        if (string.IsNullOrEmpty(email))
        {
            return;
        }

        var user = (await App.Database.GetItemsAsync<User>())
            .FirstOrDefault(u => u.EmailAddress == email);

        if (user != null)
        {
            await DisplayAlert("Password Recovery", $"Your password is: {user.Password}", "OK");
        }
        else
        {
            await DisplayAlert("Error", "No account found with this email.", "OK");
        }
    }
}
