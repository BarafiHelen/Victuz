using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Victuz.Models;
using Victuz.Views;

namespace Victuz.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private int _totalUsers;
        private int _totalEvents;
        private string _upcomingEventsText;

        public int TotalUsers
        {
            get => _totalUsers;
            set => SetProperty(ref _totalUsers, value);
        }

        public int TotalEvents
        {
            get => _totalEvents;
            set => SetProperty(ref _totalEvents, value);
        }

        public string UpcomingEventsText
        {
            get => _upcomingEventsText;
            set => SetProperty(ref _upcomingEventsText, value);
        }

        public ICommand NavigateToEventsCommand { get; }
       
        public HomeViewModel()
        {
            LoadDashboardData();
            NavigateToEventsCommand = new Command(async () => await NavigateToEvents());
            NavigateToProfileCommand = new Command(async () => await NavigateToProfile());


        }

        private async void LoadDashboardData()
        {
            var users = await App.Database.GetItemsAsync<User>();
            TotalUsers = users.Count;

            var events = await App.Database.GetItemsAsync<Event>();
            TotalEvents = events.Count;

            var upcomingEvents = events.Where(e => e.Date > DateTime.Now).ToList();
            UpcomingEventsText = $"{upcomingEvents.Count} upcoming events";

           
        }
        private async Task NavigateToEvents()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new EventView());
        }
        private async Task NavigateToProfile()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ProfileView());
        }
    }
}
