using MvvmHelpers;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Victuz.Models;
using Victuz.Services;

namespace Victuz.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        public ObservableCollection<User> Users { get; set; }
        public User SelectedUser { get; set; }

        public ICommand AddUserCommand { get; }
        public ICommand DeleteUserCommand { get; }

        public UserViewModel()
        {
            Users = new ObservableCollection<User>();
            LoadUsers();

            AddUserCommand = new Command(AddUser);
            DeleteUserCommand = new Command(DeleteUser);
        }

        private async void LoadUsers()
        {
            var users = await App.Database.GetItemsAsync<User>();
            foreach (var user in users)
            {
                Users.Add(user);
            }
        }

        private async void AddUser()
        {
            var newUser = new User
            {
                Name = "New User",
                EmailAddress = "newuser@example.com",
                PhoneNumber = "000-000-0000",
                Password = "password123"
            };

            await App.Database.SaveItemAsync(newUser);
            Users.Add(newUser);
        }

        private async void DeleteUser()
        {
            if (SelectedUser != null)
            {
                await App.Database.DeleteItemAsync(SelectedUser);
                Users.Remove(SelectedUser);
            }
        }
    }
}