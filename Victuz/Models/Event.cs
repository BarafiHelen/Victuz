using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victuz.Models
{
    public class Event
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(200), NotNull]
        public string Title { get; set; }

        [NotNull]
        public DateTime Date { get; set; }

        [MaxLength(300), NotNull]
        public string Location { get; set; }

        public string Description { get; set; }

        [NotNull]
        public int MaxParticipants { get; set; }

        

        // Constructor
        public Event()
        {
           
        }


        public void LoginUser(int userId)
        {
            Console.WriteLine($"User {userId} logged into event {Title}.");
        }

        public void LogoutUser(int userId)
        {
            Console.WriteLine($"User {userId} logged out of event {Title}.");
        }

        public int AvailablePlaces(int participantCount)
        {
            return MaxParticipants - participantCount;
        }

        public void AddParticipant(int userId)
        {
            Console.WriteLine($"User {userId} added to event {Title}.");
        }

        public void RemoveParticipant(int userId)
        {
            Console.WriteLine($"User {userId} removed from event {Title}.");
        }
    }
}
