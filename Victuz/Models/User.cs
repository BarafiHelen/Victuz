using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victuz.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<int> RegisteredEvents { get; set; }

        // Constructor
        public User()
        {
            RegisteredEvents = new List<int>();
        }

        // Method to register for an event
        public void RegisterForEvent(int eventId)
        {
            if (!RegisteredEvents.Contains(eventId))
            {
                RegisteredEvents.Add(eventId);
            }
        }
    }
}
