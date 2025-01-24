using Victuz.Views;
using Victuz.Models;

namespace Victuz.Views;

public partial class RegisterView : ContentPage
{
    public RegisterView()
    {
        InitializeComponent();
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        // Controleer of de velden zijn ingevuld
        if (string.IsNullOrEmpty(NameEntry.Text) ||
            string.IsNullOrEmpty(EmailEntry.Text) ||
            string.IsNullOrEmpty(PasswordEntry.Text) ||
            string.IsNullOrEmpty(ConfirmPasswordEntry.Text))
        {
            await DisplayAlert("Error", "Please fill in all fields", "OK");
            return;
        }

        // Controleer of het wachtwoord overeenkomt
        if (PasswordEntry.Text != ConfirmPasswordEntry.Text)
        {
            await DisplayAlert("Error", "Passwords do not match", "OK");
            return;
        }

        // Controleer of de e-mail al bestaat in de database
        var existingUser = (await App.Database.GetItemsAsync<User>())
            .FirstOrDefault(u => u.EmailAddress == EmailEntry.Text);

        if (existingUser != null)
        {
            await DisplayAlert("Error", "Email already registered", "OK");
            return;
        }

        // Nieuwe gebruiker aanmaken
        var newUser = new User
        {
            Name = NameEntry.Text,
            EmailAddress = EmailEntry.Text,
            Password = PasswordEntry.Text // Zorg ervoor dat wachtwoorden versleuteld worden
        };

        // Gebruiker opslaan in de database
        await App.Database.SaveItemAsync(newUser);

        await DisplayAlert("Success", "Registration completed", "OK");

        // Ga naar de login-pagina
        await Navigation.PushAsync(new LoginView());
    }
}
