using System;
using System.Collections.Generic;

namespace HackATL_Server.Models.Model.MongoDatabase.Users
{
    public class User : IUser
    {
        public string uID { get; set; }

        public string hackathonID { get; set; }

        public User_Personal Personal { get; set; }

        public string Team { get; set; } // teamID of user's team 

        public List<string> Connections { get; set; } // list of uid of connected users

        public string Pitch { get; set; }

        public List<string> Reservations { get; set; } // list of reservationID of reserved rooms

        public List<string> Chatrooms { get; set; } // list of chatID the user is involved in

        public User_Thread Threads { get; set; } // two types: list of threadID of favorite threads and list of personal posts

        public bool Status { get; set; } // whether user is online or not

    
    }

    public interface IUser
    {
        string uID { get; set; }

        string hackathonID { get; set; }

        User_Personal Personal { get; set; }

        string Team { get; set; }

        List<string> Connections { get; set; }

        string Pitch { get; set; }

        List<string> Reservations { get; set; }

        List<string> Chatrooms { get; set; }

        User_Thread Threads { get; set; }

        bool Status { get; set; }

        
    }

}
