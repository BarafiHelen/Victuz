using Victuz.ViewModels;

namespace Victuz.Views;

public partial class UserView : ContentPage
{
    private UserViewModel _viewModel;
    public UserView()
    {
        InitializeComponent();
        _viewModel = new UserViewModel();
        BindingContext = _viewModel;
    }
    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
    }
}