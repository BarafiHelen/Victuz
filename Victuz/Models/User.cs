using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victuz.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(100), NotNull]
        public string Name { get; set; }

        [MaxLength(15), NotNull]
        public string PhoneNumber { get; set; }

        [MaxLength(100), NotNull]
        public string Email { get; set; }

        [NotNull]
        public string Password { get; set; }
        public List<int> RegisteredEventIds { get; set; } = new List<int>();


        public void Login()
        {
            Console.WriteLine($"{Name} has logged in.");
        }
        public void EditProfile()
        {
            Console.WriteLine($"{Name} is editing their profile.");
        }
        
        // Method to register for an event
        public void RegisterForEvent(int eventId)
        {
            if (!RegisteredEventIds.Contains(eventId))
            {
                RegisteredEventIds.Add(eventId);
                Console.WriteLine($"{Name} has registered for Event {eventId}.");
            }
            else
            {
                Console.WriteLine($"{Name} is already registered for Event {eventId}.");
            }
        }

        public void CancelRegistrationForEvent(int eventId)
        {
            if (RegisteredEventIds.Contains(eventId))
            {
                RegisteredEventIds.Remove(eventId);
                Console.WriteLine($"{Name} has cancelled registration for Event {eventId}.");
            }
            else
            {
                Console.WriteLine($"{Name} is not registered for Event {eventId}.");
            }
        }
    }
}
