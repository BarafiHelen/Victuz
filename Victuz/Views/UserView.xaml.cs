using Victuz.ViewModels;

namespace Victuz.Views;

public partial class UserView : ContentPage
{
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
}