using System;
using System.Collections.Generic;

namespace HackATL_Server.Models.Model_Http.Chat
{
    public class Chat_Create
    {
        public string chatroomID { get; set; }

        public List<string> Participants { get; set; }
    }
}
