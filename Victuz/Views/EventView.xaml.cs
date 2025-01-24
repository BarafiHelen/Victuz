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
        // Laad alleen komende evenementen
        _allEvents = (await App.Database.GetItemsAsync<Event>())
                     .Where(ev => ev.Date > DateTime.Now)
                     .ToList();
        EventListView.ItemsSource = _allEvents;
    }

    private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(e.NewTextValue))
        {
            EventListView.ItemsSource = _allEvents;
        }
        else
        {
            EventListView.ItemsSource = _allEvents
                .Where(ev => ev.Title.ToLower().Contains(e.NewTextValue.ToLower()));
        }
    }

    private async void OnEventSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var selectedEvent = e.SelectedItem as Event;
        if (selectedEvent != null)
        {
            await Navigation.PushAsync(new EventDetailsView(selectedEvent));
        }
    }

    private async void OnAddEventClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddEventView());
    }
}