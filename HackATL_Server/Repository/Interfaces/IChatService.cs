using System;
using HackATL_Server.Models.Model.Chat_related;

namespace HackATL_Server.Models.Repository.Interfaces
{
    public interface IChatService
    {
        UserChat_LogList GetChatRoomList(string userId);
        ChatRoom_participants AddInitiate(AddChatRoom model);


    }
}
