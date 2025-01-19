using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Victuz.Models;

namespace Victuz.ViewModels
{
    public class EventViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Event> Events { get; set; }
        public ICommand AddEventCommand { get; }
        public ICommand DeleteEventCommand { get; }
        private readonly DatabaseService _databaseService;

        private Event _selectedEvent;
        public Event SelectedEvent
        {
            get => _selectedEvent;
            set
            {
                _selectedEvent = value;
                OnPropertyChanged();
            }
        }
        public EventViewModel()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Victuz.db");

            // DatabaseService wordt hier geïnitialiseerd met een specifiek pad
            _databaseService = new DatabaseService(dbPath);
            Events = new ObservableCollection<Event>(_databaseService.GetAll<Event>());

            AddEventCommand = new Command(AddEvent);
            DeleteEventCommand = new Command(DeleteEvent);
        }

        private void AddEvent()
        {
            var newEvent = new Event
            {
                Title = "New Event",
                Location = "Online",
                MaxParticipants = 50,
                Date = DateTime.Now.AddDays(7)
            };

            _databaseService.Save(newEvent);
            Events.Add(newEvent);
        }

        private void DeleteEvent()
        {
            if (SelectedEvent != null)
            {
                _databaseService.Delete(SelectedEvent);
                Events.Remove(SelectedEvent);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
