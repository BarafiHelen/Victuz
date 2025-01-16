using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victuz.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public void AddEvent(Event newEvent)
        {
            Console.WriteLine($"Event {newEvent.Title} added by Admin {Name}");
        }

        public void EditEvent(Event existingEvent, string newTitle)
        {
            existingEvent.Title = newTitle;
            Console.WriteLine($"Event {existingEvent.Id} updated by Admin {Name}");
        }

        public void DeleteEvent(Event eventToDelete)
        {
            Console.WriteLine($"Event {eventToDelete.Id} deleted by Admin {Name}");
        }
    }
}
