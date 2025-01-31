using System.Collections.ObjectModel;
using Victuz.Models;
using Victuz.ViewModels;

namespace Victuz.Views;

public partial class UserView : ContentPage
{
    public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();

    public UserView()
    {
        InitializeComponent();
        BindingContext = new UserViewModel();
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        if (BindingContext is UserViewModel viewModel)
        {
            viewModel.SearchText = e.NewTextValue;
        }
    }
    private async void OnAddUserClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddUserView());
    }

    private async void OnRemoveUserClicked(object sender, EventArgs e)
    {
        if (BindingContext is UserViewModel vm && vm.SelectedUser != null)
        {
            bool confirm = await DisplayAlert("Confirm", $"Are you sure you want to delete {vm.SelectedUser.Name}?", "Yes", "No");
            if (confirm)
            {
                vm.DeleteUserCommand.Execute(null);
            }
        }
        else
        {
            await DisplayAlert("Error", "No user selected to remove.", "OK");
        }
    }
}
