using System;
namespace HackATL_Server.Models.Model.MongoDatabase.Chats
{
    public class Message
    {
        public string senderID { get; set; }

        public string Text { get; set; }

        public DateTime Time { get; set; }

    }
}
