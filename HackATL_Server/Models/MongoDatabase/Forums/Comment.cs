using System;
namespace HackATL_Server.Models.Model.MongoDatabase.Forums
{
    public class Comment
    {

        public string uID { get; set; } // uid of user who wrote the post 

        public string Text { get; set; } // Comment 

        public DateTime Time { get; set; }
    }
}
