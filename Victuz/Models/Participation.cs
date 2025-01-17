﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victuz.Models
{
    public class Participation
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed, NotNull]
        public int UserId { get; set; }

        [Indexed, NotNull]
        public int EventId { get; set; }

        [NotNull]
        public DateTime ParticipationDate { get; set; }

        public Participation(int userId, int eventId)
        {
            UserId = userId;
            EventId = eventId;
            ParticipationDate = DateTime.Now;
        }
        // Methode om deelname te annuleren
        public void CancelParticipation()
        {
            Console.WriteLine($"Participation for User {UserId} in Event {EventId} has been cancelled.");
        }
    }
}
