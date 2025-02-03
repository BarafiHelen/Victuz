using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Victuz.Models;

namespace Victuz.Services
{
    public class WeatherApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather?q=Amsterdam&appid=0451746b92855b88bc7fd813d9ddd9b2&units=metric"; // Gebruik je API-sleutel

        public WeatherApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetWeatherAsync()
        {
            try
            {
                Console.WriteLine("🚀 API Call gestart...");
                var response = await _httpClient.GetAsync(BaseUrl);

                // Controleer of de respons succesvol is
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"🌍 Response ontvangen: {json}");

                    // Deserialiseer de response naar WeatherResponse
                    var result = JsonSerializer.Deserialize<WeatherResponse>(json);
                    if (result?.Weather != null && result?.Weather.Count > 0 && result?.Main != null)
                    {
                        // Return de beschrijving van het weer en de temperatuur
                        return $"Het weer is {result.Weather[0].Description} en de temperatuur is {result.Main.Temp}°C.";
                    }
                    else
                    {
                        return "❌ Geen geldige weerdata ontvangen (null of lege lijst).";
                    }
                }
                else
                {
                    return $"❌ API-fout: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                return $"❌ Fout bij het ophalen van de API: {ex.Message}";
            }
        }
    }
}
