using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HackATL_Server.Models.Model.MongoDatabase.Chats
{
    public class Chatroom 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string chatID { get; set; }

        public List<string> Participants { get; set; } // list of uid of users in chatroom 

        public List<Message> Logs { get; set; } // chatlog 
    }

}
