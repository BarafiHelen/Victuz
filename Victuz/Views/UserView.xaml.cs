using Victuz.ViewModels;

namespace Victuz.Views;

public partial class UserView : ContentPage
{
    public UserView()
    {
        InitializeComponent();
        BindingContext = new UserViewModel();
    }
}