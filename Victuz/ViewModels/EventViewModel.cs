using MvvmHelpers;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Victuz.Models;
using Victuz.Services;

namespace Victuz.ViewModels
{
    public class EventViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService;

        public ObservableCollection<Event> Events { get; set; } = new ObservableCollection<Event>();

        private Event _selectedEvent;
        public Event SelectedEvent
        {
            get => _selectedEvent;
            set
            {
                SetProperty(ref _selectedEvent, value);
                OnEventSelected(value);
            }
        }

        public Command AddEventCommand { get; }
        public Command DeleteEventCommand { get; }
        public Command LoadEventsCommand { get; }

        public EventViewModel()
        {
            _databaseService = App.Database;

            AddEventCommand = new Command(async () => await AddEvent());
            DeleteEventCommand = new Command(async () => await DeleteEvent(), () => SelectedEvent != null);
            LoadEventsCommand = new Command(async () => await LoadEvents());

            LoadEvents();
        }

        private async Task LoadEvents()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;

                Console.WriteLine("Loading events from the database...");
                var events = await _databaseService.GetItemsAsync<Event>();

                if (events != null)
                {
                    Events.Clear();
                    foreach (var ev in events)
                    {
                        Console.WriteLine($"Loaded Event: {ev.Title}, {ev.Date}");
                        Events.Add(ev);
                    }
                }
                else
                {
                    Console.WriteLine("No events found in the database.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading events: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task AddEvent()
        {
            var newEvent = new Event
            {
                Title = "New Event",
                Date = DateTime.Now.AddDays(7),
                Time = "18:00",
                Location = "Online",
                Description = "Description of the new event",
                MaxParticipants = 50
            };

            await _databaseService.SaveItemAsync(newEvent);
            Events.Add(newEvent);
        }

        private async Task DeleteEvent()
        {
            if (SelectedEvent == null) return;

            await _databaseService.DeleteItemAsync(SelectedEvent);
            Events.Remove(SelectedEvent);
        }

        private void OnEventSelected(Event eventItem)
        {
            if (eventItem == null)
                return;

            // Hier kun je de logica toevoegen wanneer een event is geselecteerd
        }
    }
}