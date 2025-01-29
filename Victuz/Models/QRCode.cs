using Microsoft.VisualBasic;
using SQLite;

namespace Victuz.Models
{
    [Table("QRCodes")]
    public class QRCode
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int EventID { get; set; }
        public int UserID { get; set; }

        [MaxLength(50)]
        public string Code { get; set; }
        public string QRCodeContent { get; set; } // De inhoud van de QR-code
        public byte[] QRCodeImage { get; set; }   // De afbeelding van de QR-code
        // QR-code genereren
        public string Generate()
        {
            Code = "QR-" + Guid.NewGuid().ToString();
            return Code;
        }

        // QR-code valideren
        public bool Validate(string inputCode)
        {
            return Code == inputCode;
        }
    }
}
