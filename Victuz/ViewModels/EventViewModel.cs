using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Victuz.Models;
using Victuz.Services;
using System.Threading.Tasks;

namespace Victuz.ViewModels
{
    public class EventViewModel : BaseViewModel
    {
        public ObservableCollection<Event> Events { get; set; } = new ObservableCollection<Event>();

        public Command AddEventCommand { get; }
        public Command DeleteEventCommand { get; }
        public Command LoadPublicEventsCommand { get; }

        private readonly OpenEventApiService _eventApiService = new OpenEventApiService();

        private Event _selectedEvent;
        public Event SelectedEvent
        {
            get => _selectedEvent;
            set => SetProperty(ref _selectedEvent, value);
        }

        public EventViewModel()
        {
            LoadEvents();
            AddEventCommand = new Command(OnAddEvent);
            DeleteEventCommand = new Command(OnDeleteEvent, CanDeleteEvent);
            LoadPublicEventsCommand = new Command(async () => await LoadPublicEvents());
        }

        private async void LoadEvents()
        {
            try
            {
                var events = await App.Database.GetItemsAsync<Event>();
                Events.Clear();
                foreach (var evt in events)
                {
                    Events.Add(evt);
                }
            }
            catch (Exception)
            {
                // Log de fout of toon een melding
            }
        }

        private async void OnAddEvent()
        {
            var newEvent = new Event
            {
                Title = "New Event",
                Location = "Online",
                MaxParticipants = 50,
                Date = DateTime.Now.AddDays(7)
            };

            await App.Database.SaveItemAsync(newEvent);
            Events.Add(newEvent);
        }

        private bool CanDeleteEvent()
        {
            return SelectedEvent != null;
        }

        //Pop API Fix

        private async void OnDeleteEvent()
        {
            if (SelectedEvent != null)
            {
                await App.Database.DeleteItemAsync(SelectedEvent);
                Events.Remove(SelectedEvent);
            }
        }

        private async Task LoadPublicEvents()
        {
            var publicEvents = await _eventApiService.GetPublicEventsAsync();
            foreach (var evt in publicEvents)
            {
                Events.Add(evt);
            }
        }
    }
}
