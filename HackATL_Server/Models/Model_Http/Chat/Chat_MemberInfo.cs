using System;
using System.Collections.Generic;
using HackATL_Server.Models.Model.MongoDatabase.Users;

namespace HackATL_Server.Models.Model_Http.Chat
{
    public class Chat_MemberInfo
    {
        public string chatroomID { get; set; }

        public List<User_Personal> MemberInfos { get; set; }
    }
}
