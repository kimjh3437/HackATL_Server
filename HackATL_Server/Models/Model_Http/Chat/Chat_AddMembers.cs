using System;
using System.Collections.Generic;

namespace HackATL_Server.Models.Model_Http.Chat
{
    public class Chat_AddMembers
    {
        public string uID { get; set; } // host

        public string chatroomID { get; set; }

        public List<string> Members { get; set; }
    }
}
