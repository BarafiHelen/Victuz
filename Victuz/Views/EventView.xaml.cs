using System.Collections.ObjectModel;
using Victuz.Models;
using Victuz.ViewModels;

namespace Victuz.Views;

public partial class EventView : ContentPage
{
    public ObservableCollection<Event> Events { get; set; } = new ObservableCollection<Event>();
    

    public ListView EventListViewPublic => EventListView;

    public EventView()
    {
        InitializeComponent();
        BindingContext = new EventViewModel(); ;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadEvents(); 
    }

    private async Task LoadEvents()
    {
        try
        {
            var events = await App.Database.GetItemsAsync<Event>();

            if (events != null)
            {
                Events.Clear();

                foreach (var ev in events.Where(e => e.Date > DateTime.Now))
                {
                    Events.Add(ev);
                }

                EventListView.ItemsSource = null; // Forceer UI-verversing
                EventListView.ItemsSource = Events;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading events: {ex.Message}");
        }
    }

    private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
    {
        var filteredEvents = string.IsNullOrWhiteSpace(e.NewTextValue)
            ? Events
            : new ObservableCollection<Event>(Events.Where(ev => ev.Title.ToLower().Contains(e.NewTextValue.ToLower())));

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
                Events.Remove(selectedEvent);
                EventListView.ItemsSource = new ObservableCollection<Event>(Events); // Zorg voor directe update
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
        await LoadEvents(); // Update de lijst na het bewerken
    }

    private async void OnEventSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Event selectedEvent)
        {
            await Navigation.PushAsync(new EventDetailsView(selectedEvent));
        }
    }
}
