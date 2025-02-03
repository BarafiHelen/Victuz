using Microsoft.Extensions.Logging;
using Victuz.Models;

namespace Victuz.Views;

public partial class AddEventView : ContentPage
{
    public AddEventView()
    {
        InitializeComponent();
    }

    private async void OnAddEventClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TitleEntry.Text) ||
            string.IsNullOrWhiteSpace(TimeEntry.Text) ||
            string.IsNullOrWhiteSpace(LocationEntry.Text) ||
            string.IsNullOrWhiteSpace(MaxParticipantsEntry.Text))
        {
            await DisplayAlert("Error", "Please fill in all fields", "OK");
            return;
        }

        if (!int.TryParse(MaxParticipantsEntry.Text, out int maxParticipants) || maxParticipants <= 0)
        {
            await DisplayAlert("Error", "Max Participants must be a valid number greater than 0", "OK");
            return;
        }

        var newEvent = new Event
        {
            Title = TitleEntry.Text,
            Time = TimeEntry.Text,
            Date = DatePicker.Date,
            Location = LocationEntry.Text,
            Description = DescriptionEditor.Text ?? string.Empty,
            MaxParticipants = maxParticipants
        };

        await App.Database.SaveItemAsync(newEvent);
        await DisplayAlert("Success", "Event added successfully!", "OK");
       
        // Controleer of het evenement is opgeslagen
        var allEvents = await App.Database.GetItemsAsync<Event>();
        foreach (var evt in allEvents)
        {
            Console.WriteLine($"Event in database: {evt.Title}, {evt.Date}");
       
        }
       

        if (Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault() is EventView eventView)
        {
            eventView.Events.Add(newEvent);
            eventView.EventListViewPublic.ItemsSource = null; // Forceer UI-verversing
            eventView.EventListViewPublic.ItemsSource = eventView.Events;
        }
        await Navigation.PopAsync(); // Keer terug en laat EventView de lijst updaten
    }
}
