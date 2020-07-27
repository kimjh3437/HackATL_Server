using System;
using HackATL_Server.Models.Model.MongoDatabase.Forums;

namespace HackATL_Server.Models.Model_Http.Forum
{
    public class Thread_Create
    {
        public string uID { get; set; }

        public Thread_Detail Detail { get; set; }
    }
}
