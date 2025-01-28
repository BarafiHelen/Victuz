using Victuz.Models;

namespace Victuz.Views;

public partial class EventView : ContentPage
{
    private List<Event> _allEvents;

    public EventView()
    {
        InitializeComponent();
        LoadEvents();
    }

    private async void LoadEvents()
    {
        _allEvents = (await App.Database.GetItemsAsync<Event>())
                     .Where(ev => ev.Date > DateTime.Now)
                     .ToList();
        EventListView.ItemsSource = _allEvents;
    }

    private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
    {
        var filteredEvents = string.IsNullOrWhiteSpace(e.NewTextValue)
            ? _allEvents
            : _allEvents.Where(ev => ev.Title.ToLower().Contains(e.NewTextValue.ToLower())).ToList();

        EventListView.ItemsSource = filteredEvents;
    }

    private async void OnAddEventClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddEventView());
    }

    private async void OnRemoveEventClicked(object sender, EventArgs e)
    {
        if (EventListView.SelectedItem is Event selectedEvent)
        {
            bool confirm = await DisplayAlert("Confirm", $"Are you sure you want to delete {selectedEvent.Title}?", "Yes", "No");
            if (confirm)
            {
                await App.Database.DeleteItemAsync(selectedEvent);
                _allEvents.Remove(selectedEvent);
                EventListView.ItemsSource = new List<Event>(_allEvents);
            }
        }
        else
        {
            await DisplayAlert("Error", "Please select an event to remove.", "OK");
        }
    }

    private async void OnEditEventClicked(object sender, EventArgs e)
    {
        if (EventListView.SelectedItem is not Event selectedEvent)
        {
            await DisplayAlert("Error", "Please select an event to edit.", "OK");
            return;
        }

        string newTitle = await DisplayPromptAsync("Edit Event", "Enter new title:", initialValue: selectedEvent.Title);
        if (string.IsNullOrWhiteSpace(newTitle))
            return;

        string newDateStr = await DisplayPromptAsync("Edit Event", "Enter new date (yyyy-MM-dd):", initialValue: selectedEvent.Date.ToString("yyyy-MM-dd"));
        if (!DateTime.TryParse(newDateStr, out DateTime newDate))
        {
            await DisplayAlert("Error", "Invalid date format!", "OK");
            return;
        }

        selectedEvent.Title = newTitle;
        selectedEvent.Date = newDate;

        await App.Database.SaveItemAsync(selectedEvent);
        EventListView.ItemsSource = new List<Event>(_allEvents); // Refresh list
    }

    private async void OnEventSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Event selectedEvent)
        {
            await Navigation.PushAsync(new EventDetailsView(selectedEvent));
        }
    }
}
