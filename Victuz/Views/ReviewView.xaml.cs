namespace Victuz.Views;

public partial class ReviewView : ContentPage
{
    public ReviewView()
    {
        InitializeComponent();
    }

    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        // Placeholder logica voor het opslaan van een beoordeling
        await DisplayAlert("Success", "Thank you for your review!", "OK");
    }
}