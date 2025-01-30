using MvvmHelpers;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Victuz.Models;
using Victuz.Services;
namespace Victuz.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        public ObservableCollection<User> Users { get; set; }
        public User SelectedUser { get; set; }
        public string SearchText { get; set; }

        public ICommand AddUserCommand { get; }
        public ICommand DeleteUserCommand { get; }

        public UserViewModel()
        {
            Users = new ObservableCollection<User>();
            AddUserCommand = new Command(async () => await AddUserAsync());
            DeleteUserCommand = new Command(async () => await DeleteUserAsync(), () => SelectedUser != null);

            LoadUsers();
        }

        private async void LoadUsers()
        {
            // Haal alle gebruikers op uit de database
            var userList = await App.Database.GetItemsAsync<User>();
            Users.Clear();

            // Voeg elke gebruiker handmatig toe aan de ObservableCollection
            for (int i = 0; i < userList.Count; i++)
            {
                Users.Add(userList[i]);
            }
        }

        private async Task AddUserAsync()
        {
            // Maak een nieuwe gebruiker aan
            var newUser = new User
            {
                Name = "New User",
                EmailAddress = "newuser@example.com",
                PhoneNumber = "000-000-0000",
                Password = "password123"
            };

            // Sla de gebruiker op in de database
            await App.Database.SaveItemAsync(newUser);

            // Voeg de nieuwe gebruiker toe aan de ObservableCollection
            Users.Add(newUser);
        }

        private async Task DeleteUserAsync()
        {
            if (SelectedUser != null)
            {
                // Verwijder de geselecteerde gebruiker uit de database
                await App.Database.DeleteItemAsync(SelectedUser);

                // Verwijder de gebruiker uit de ObservableCollection
                Users.Remove(SelectedUser);

                // Wis de selectie
                SelectedUser = null;
            }
        }
    }
}
