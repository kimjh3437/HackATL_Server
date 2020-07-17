using System;
using System.Collections.Generic;

namespace HackATL_Server.Models.Model.MongoDatabase.Users
{
    public class User_Thread
    {
        public string uID { get; set; }

        public List<string> Favorites { get; set; } // list of favorited threads 

        public List<string> MyThreads { get; set; } // list of mythreads 
    }
}
