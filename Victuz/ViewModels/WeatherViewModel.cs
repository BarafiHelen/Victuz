using MvvmHelpers;
using System;
using Victuz.Services;
using System.Threading.Tasks;

namespace Victuz.ViewModels
{
    public class WeatherViewModel : BaseViewModel
    {
        public string WeatherDescription { get; set; }

        private readonly WeatherApiService _weatherApiService = new WeatherApiService();

        public Command LoadWeatherCommand { get; }

        public WeatherViewModel()
        {
            LoadWeatherCommand = new Command(async () => await LoadWeather());
        }

        private async Task LoadWeather()
        {
            WeatherDescription = await _weatherApiService.GetWeatherAsync();
            OnPropertyChanged(nameof(WeatherDescription));
        }
    }
}
