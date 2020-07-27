using System;
using System.Collections.Generic;

namespace HackATL_Server.Models.Model.MongoDatabase.Reservations
{
    public class Rooms
    {
        public string roomID { get; set; } // key roomID 

        public string Location { get; set; }

        public int Capacity { get; set; }

        public bool Status { get; set; }

        public List<string> Reservations { get; set; } // list of reservationID associated with this room
    }
}
