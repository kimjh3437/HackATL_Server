using System;
using System.Collections.Generic;
using HackATL_Server.Models.Model.MongoDatabase.Chats;
using HackATL_Server.Models.Model.MongoDatabase.Users;

namespace HackATL_Server.Repository.Interfaces_MongoDB
{
    public interface IChatService_md
    {
        Boolean AddMember(string uID, string chatID);

        List<User_Personal> CreateChatroom(List<string> membersID);

        Boolean DeleteChatroom(string uID, string chatID);

        List<Chatroom> GetChatrooms(string uID);

        List<User_Personal> GetMembers(List<string> members);


    }
}
