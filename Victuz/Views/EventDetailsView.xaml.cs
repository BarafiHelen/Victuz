using Victuz.Models;

namespace Victuz.Views;

public partial class EventDetailsView : ContentPage
{
    private Event _event;

    public EventDetailsView(Event selectedEvent)
    {
        InitializeComponent();
        BindingContext = selectedEvent;

    }

    private async void OnJoinClicked(object sender, EventArgs e)
    {
        // Save participation in database and navigate back
        await App.Database.SaveItemAsync(new Participation
        {
            EventID = _event.ID,
            UserID = 1, // Placeholder for logged-in user
            RegisterDate = DateTime.Now
        });

        await DisplayAlert("Success", "You have joined the event!", "OK");
        await Navigation.PopAsync();
    }
}