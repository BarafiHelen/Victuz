using Victuz.ViewModels;
using System;

namespace Victuz.Views
{
    public partial class ApiView : ContentPage
    {
        private WeatherViewModel _viewModel;

        public ApiView()
        {
            InitializeComponent();
            _viewModel = new WeatherViewModel();
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Console.WriteLine("📌 ApiView verschijnt! Laden van weergegevens...");
            _viewModel.LoadWeatherCommand.Execute(null);
        }
    }
}
