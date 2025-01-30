using MvvmHelpers;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Victuz.Models;
using Victuz.Services;

namespace Victuz.ViewModels;

public class UserViewModel : BaseViewModel
{
    public ObservableCollection<User> Users { get; private set; } = new ObservableCollection<User>();
    public ObservableCollection<User> FilteredUsers { get; private set; } = new ObservableCollection<User>();

    private string _searchText;
    public string SearchText
    {
        get => _searchText;
        set
        {
            if (SetProperty(ref _searchText, value))
            {
                FilterUsers();
            }
        }
    }

    private User _selectedUser;
    public User SelectedUser
    {
        get => _selectedUser;
        set
        {
            if (SetProperty(ref _selectedUser, value))
            {
                IsDeleteEnabled = value != null;
            }
        }
    }

    private bool _isDeleteEnabled;
    public bool IsDeleteEnabled
    {
        get => _isDeleteEnabled;
        set => SetProperty(ref _isDeleteEnabled, value);
    }

    public ICommand AddUserCommand { get; }
    public ICommand DeleteUserCommand { get; }

    public UserViewModel()
    {
        LoadUsers();

        AddUserCommand = new Command(AddUser);
        DeleteUserCommand = new Command(DeleteUser);
    }

    private async void LoadUsers()
    {
        var users = await App.Database.GetItemsAsync<User>();
        Users.Clear();
        foreach (var user in users)
        {
            Users.Add(user);
        }
        FilterUsers();
    }

    private void FilterUsers()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            FilteredUsers.Clear();
            foreach (var user in Users)
            {
                FilteredUsers.Add(user);
            }
        }
        else
        {
            var filtered = Users.Where(u => u.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                            u.EmailAddress.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            FilteredUsers.Clear();
            foreach (var user in filtered)
            {
                FilteredUsers.Add(user);
            }
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
        FilterUsers();
    }

    private async void DeleteUser()
    {
        if (SelectedUser != null)
        {
            await App.Database.DeleteItemAsync(SelectedUser);
            Users.Remove(SelectedUser);
            FilterUsers();
        }
    }
}