using SQLite;

namespace Victuz.Models
{
    [Table("Events")]
    public class Event
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        public DateTime Date { get; set; }
        [MaxLength(20)]
        public string Time { get; set; }
        [MaxLength(100)]
        public string Location { get; set; }
        public string Description { get; set; }
        public int MaxParticipants { get; set; }

        // Beschikbare plaatsen berekenen
        public int AvailablePlaces(int currentParticipants)
        {
            return MaxParticipants - currentParticipants;
        }
    }
}