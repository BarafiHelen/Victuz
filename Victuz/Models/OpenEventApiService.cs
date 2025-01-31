using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Victuz.Models;

namespace Victuz.Services
{
    public class OpenEventApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://open-event-api.herokuapp.com/events";
        //Geen API Key nodig

        public OpenEventApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Event>> GetPublicEventsAsync()
        {
            var response = await _httpClient.GetAsync(BaseUrl);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<OpenEvent>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                List<Event> events = new List<Event>();
                if (result != null)
                {
                    foreach (var evt in result)
                    {
                        events.Add(new Event
                        {
                            Title = evt.Name,
                            Location = evt.Location ?? "Onbekende locatie",
                            Date = DateTime.Parse(evt.StartDate),
                            Description = evt.Description ?? "Geen beschrijving beschikbaar.",
                            MaxParticipants = evt.Capacity ?? 100
                        });
                    }
                }
                return events;
            }
            return new List<Event>();
        }
    }

    // Model om API-response te verwerkeno
    public class OpenEvent
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string StartDate { get; set; }
        public string Description { get; set; }
        public int? Capacity { get; set; }
    }
}
