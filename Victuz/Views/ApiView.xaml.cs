using Victuz.ViewModels;

namespace Victuz.Views;

public partial class ApiView : ContentPage
{
    public ApiView()
    {
        InitializeComponent();
        BindingContext = new EventViewModel();  // Zorg ervoor dat de ViewModel wordt gekoppeld
    }
}
