using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HackATL_Server.Models.Model.MongoDatabase.Agenda
{
    public class Agenda 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public string agendaID { get; set; }

        public string Name { get; set; }

        public DateTime Time { get; set; }

        public string Location { get; set; }

        public string Day { get; set; } // maybe this needs to be DateTime?

        public Agenda_Detail Detail { get; set; }

        public List<string> Participants { get; set; } // list of uid of users that add this event to their own agenda 
    }
}
