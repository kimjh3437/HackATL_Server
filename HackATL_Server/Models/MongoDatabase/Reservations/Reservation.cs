using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HackATL_Server.Models.Model.MongoDatabase.Reservations
{
    public class Reservation 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string reservationID { get; set; } // key reservationID 

        public string roomID { get; set; } // roomID associated with this reservation 

        public string uID { get; set; } // user that booked this room 

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime Date { get; set; } //todays day : friday, saturday, etc 
    }

  

}
