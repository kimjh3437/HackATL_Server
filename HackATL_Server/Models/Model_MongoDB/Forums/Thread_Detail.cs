﻿using System;
using System.Collections.Generic;

namespace HackATL_Server.Models.Model.MongoDatabase.Forums
{
    public class Thread_Detail
    {
        public string threadID { get; set;
        }
        public string Title { get; set; }

        public DateTime TimePosted { get; set; }

        public string Contents { get; set; }

        public string Day { get; set; }

        public string Author { get; set; } // uid of author : user

        public string Category { get; set; }

        
    }
}
