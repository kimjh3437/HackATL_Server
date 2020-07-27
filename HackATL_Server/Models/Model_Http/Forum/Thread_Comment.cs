using System;
using HackATL_Server.Models.Model.MongoDatabase.Forums;

namespace HackATL_Server.Models.Model_Http.Forum
{
    public class Thread_Comment
    {
        public string threadID { get; set; }

        public Comment Comment { get; set; }
    }
}
