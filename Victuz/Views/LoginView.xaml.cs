using Victuz.Models;

namespace Victuz.Views;

public partial class LoginView : ContentPage
{
    private bool _isPasswordVisible = false; // Houdt bij of het wachtwoord zichtbaar is

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
            // Sla de status op als de gebruiker "Stay logged in" heeft aangevinkt
            if (StayLoggedInCheckBox.IsChecked)
            {
                await SecureStorage.SetAsync("LoggedInUserID", user.ID.ToString());
            }

            await Navigation.PushAsync(new HomeView());
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

    private void OnTogglePasswordClicked(object sender, EventArgs e)
    {
        _isPasswordVisible = !_isPasswordVisible;
        PasswordEntry.IsPassword = !_isPasswordVisible;

        // Oog-icoon updaten
        TogglePasswordButton.Text = _isPasswordVisible ? "🙈" : "👁";
    }
}
