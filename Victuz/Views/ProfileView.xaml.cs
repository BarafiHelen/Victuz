using MvvmHelpers;
using System.Windows.Input;

namespace Victuz.ViewModels
{
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
            Name = "John Doe";
            Email = "john.doe@example.com";
            PhoneNumber = "123-456-7890";

            SaveCommand = new Command(SaveProfile);
        }

        private void SaveProfile()
        {
            // Opslaan in de database of andere logica
            App.Current.MainPage.DisplayAlert("Success", "Profile updated!", "OK");
        }
    }
}