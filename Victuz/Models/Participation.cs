using SQLite;

namespace Victuz.Models
{
    [Table("Participations")]
    public class Participation
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int UserID { get; set; }
        public int EventID { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime JoinDate { get; internal set; }
    }
}
