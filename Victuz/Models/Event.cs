using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victuz.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public List<int> Participants { get; set; }

        // Constructor
        public Event()
        {
            Participants = new List<int>();
        }

        // Method to add a participant
        public bool AddParticipant(int userId)
        {
            if (Participants.Count < Capacity)
            {
                Participants.Add(userId);
                return true;
            }
            return false;
        }
    }
}
