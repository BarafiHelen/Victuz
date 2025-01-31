using Victuz.Models;

namespace Victuz.Views;

public partial class AddUserView : ContentPage
{
    public AddUserView()
    {
        InitializeComponent();
    }

    private async void OnSaveUserClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameEntry.Text) ||
            string.IsNullOrWhiteSpace(EmailEntry.Text) ||
            string.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            await DisplayAlert("Error", "Please fill in all required fields.", "OK");
            return;
        }

        var newUser = new User
        {
            Name = NameEntry.Text,
            EmailAddress = EmailEntry.Text,
            PhoneNumber = PhoneNumberEntry.Text,
            Password = PasswordEntry.Text
        };

        var existingUser = (await App.Database.GetItemsAsync<User>())
            .FirstOrDefault(u => u.EmailAddress == newUser.EmailAddress);

        if (existingUser != null)
        {
            await DisplayAlert("Error", "A user with this email already exists.", "OK");
            return;
        }

        await App.Database.SaveItemAsync(newUser);
        await DisplayAlert("Success", "User added successfully!", "OK");

        await Navigation.PopAsync();
    }
}
