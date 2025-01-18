using Microsoft.Extensions.Logging;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victuz.Models
{
    public class QRCode
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed, NotNull]
        public int EventId { get; set; }

        [MaxLength(300), NotNull]
        public string Code { get; set; }
        public DateTime ExpiryDate { get; set; }

        public QRCode(int eventId)
        {
            EventId = eventId;
            Generate();
        }
        public void Generate()
        {
            Code = Guid.NewGuid().ToString();
            ExpiryDate = DateTime.Now.AddDays(7);   // QR-code verloopt na 7 dagen 
            Console.WriteLine($"QR Code generated: {Code}, valid until{ExpiryDate}");
        }

        public bool IsValid()
        {
            return DateTime.Now <= ExpiryDate;
        }
    }
}
