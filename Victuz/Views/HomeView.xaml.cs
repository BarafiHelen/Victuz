using Victuz.ViewModels;

namespace Victuz.Views;

public partial class HomeView : ContentPage
{
	public HomeView()
	{
		InitializeComponent();
        BindingContext = new HomeViewModel();
    }
}