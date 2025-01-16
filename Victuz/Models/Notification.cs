using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victuz.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public int UserId { get; set; }

        public Notification(string message, int userId)
        {
            Message = message;
            UserId = userId;
            Timestamp = DateTime.Now;
        }

        public void SendNotification()
        {
            Console.WriteLine($"Notification to User {UserId}: {Message}");
        }
    }
}
