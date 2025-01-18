using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victuz.Models
{
    public class Notification
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed, NotNull]
        public int UserId { get; set; }

        [MaxLength(500), NotNull]
        public string Message { get; set; }

        [NotNull]
        public DateTime ShippingDate { get; set; }

        public Notification(string message, int userId)
        {
            Message = message;
            UserId = userId;
            ShippingDate = DateTime.Now;
        }

        public void SendNotification()
        {
            Console.WriteLine($"Notification to User {UserId}: {Message}");
        }
       
    }
}
