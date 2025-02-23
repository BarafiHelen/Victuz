﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victuz.Models;

namespace Victuz.Models
{
    public class Admin : User
    {

        public void AddEvent(Event newEvent)
        {
            Console.WriteLine($"Event {newEvent.Title} added by Admin {Name}");
        }

        public void UpdateEvent(Event existingEvent, string newTitle)
        {
            existingEvent.Title = newTitle;
            Console.WriteLine($"Event {existingEvent.ID} updated by Admin {Name}");
        }

        public void DeleteEvent(Event eventToDelete)
        {
            Console.WriteLine($"Event {eventToDelete.ID} deleted by Admin {Name}");
        }
    }
}
