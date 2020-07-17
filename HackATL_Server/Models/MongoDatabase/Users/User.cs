using System;
using System.Collections.Generic;
using HackATL_Server.Models.Model.MongoDatabase.Auth;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HackATL_Server.Models.Model.MongoDatabase.Users
{
    public class User 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public string uID { get; set; }

        public string hackathonID { get; set; }

        public User_Personal Personal { get; set; }

        public string Team { get; set; } // teamID of user's team 

        public List<string> Connections { get; set; } // list of uid of connected users

        public string Pitch { get; set; }

        public List<string> Reservations { get; set; } // list of reservationID of reserved rooms

        public List<string> Chatrooms { get; set; } // list of chatID the user is involved in

        public List<string> Agendas { get; set; } // list of agendaID in MyAgenda 

        public User_Thread Threads { get; set; } // two types: list of threadID of favorite threads and list of personal posts

        public bool Status { get; set; } // whether user is online or not

        public AuthUser Auth { get; set; }

        public string Token { get; set; }

        public string Role { get; set; }

    
    }


}
