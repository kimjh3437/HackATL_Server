using System;
using System.Collections.Generic;
using HackATL_Server.Models.Model.MongoDatabase.Chats;
using HackATL_Server.Models.Model_Http.Chat;

namespace HackATL_Server.Repos.Interface
{
    public interface IChatService
    {
        Chatroom CreateChat(Chat_Create create);

        Chatroom GetChat(string chatroomID);

        Chat_MemberInfo GetMembersInfo_Single(string chatroomID);

        List<Chat_MemberInfo> GetMembersInfo_All(List<string> chatroomIDs);

        Boolean LeaveChat(Chat_Leave leave);
    }
}
