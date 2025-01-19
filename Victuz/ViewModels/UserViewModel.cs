using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Victuz.Models;

namespace Victuz.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<User> Users { get; set; }
        public ICommand AddUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        private readonly DatabaseService _databaseService;

        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
            }

        }
        public UserViewModel()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Victuz.db");

            // DatabaseService wordt hier geïnitialiseerd met een specifiek pad
            _databaseService = new DatabaseService(dbPath);
            Users = new ObservableCollection<User>(_databaseService.GetAll<User>());

            AddUserCommand = new Command(AddUser);
            DeleteUserCommand = new Command(DeleteUser);
        }

        private void AddUser()
        {
            var newUser = new User
            {
                Name = "New User",
                PhoneNumber = "123456789",
                Email = "newuser@example.com",
                Password = "password123"
            };

            _databaseService.Save(newUser);
            Users.Add(newUser);
        }
        private void DeleteUser()
        {
            if (SelectedUser != null)
            {
                _databaseService.Delete(SelectedUser);
                Users.Remove(SelectedUser);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
