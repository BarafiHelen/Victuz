using SQLite;

namespace Victuz.Models
{
    [Table("Notifications")]
    public class Notification
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Message { get; set; }
        public DateTime ShippingDate { get; set; }

        // Melding sturen
        public void SendNotification(string message)
        {
            Message = message;
            ShippingDate = DateTime.Now;
        }
    }
}