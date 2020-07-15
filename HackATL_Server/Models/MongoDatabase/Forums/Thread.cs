using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HackATL_Server.Models.Model.MongoDatabase.Forums
{
    public class Thread 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string threadID { get; set; }

        public bool Favorite { get; set; } //always null 

        public Thread_Detail Detail { get; set; }

        public List<Comment> Comments { get; set; }
    }


}
