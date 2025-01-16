using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victuz.Models
{
    public class Registration
    {
        public int UserId { get; set; }
        public int EventId { get; set; }
        public DateTime RegistrationDate { get; set; }

        public Registration(int userId, int eventId)
        {
            UserId = userId;
            EventId = eventId;
            RegistrationDate = DateTime.Now;
        }

        public void Register()
        {
            Console.WriteLine($"User {UserId} registered for Event {EventId} on {RegistrationDate}");
        }
    }
}
