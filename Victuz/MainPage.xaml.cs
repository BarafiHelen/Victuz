using Victuz.Models;

namespace Victuz
{
    public partial class MainPage : ContentPage
    {


        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public Command<string> NavigateCommand => new(async (route) =>
        {
            await Shell.Current.GoToAsync(route);
        });
       
        // Override OnAppearing
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Test: Voeg een gebruiker toe en haal hem op
            var testUser = new User
            {
                Name = "Test User",
                EmailAddress = "test@example.com",
                Password = "1234"
            };

            await App.Database.SaveItemAsync(testUser);

            var users = await App.Database.GetItemsAsync<User>();
            Console.WriteLine($"Total users in database: {users.Count}");
        }
    }
}
