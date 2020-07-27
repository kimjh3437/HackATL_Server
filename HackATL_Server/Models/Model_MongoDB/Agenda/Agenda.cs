using System;
using System.Collections.Generic;
using HackATL_Server.Models.Model_General;
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

        public string Title { get; set; }

        public Agenda_Detail Detail { get; set; }

        public List<string> Participants { get; set; } // list of uid of users that add this event to their own agenda 
    }

    
}
