using System;
using System.Collections.Generic;
using System.Linq;
using HackATL_Server.Helper;
using HackATL_Server.Models.Model.Chat_related;
using HackATL_Server.Models.Repository.Interfaces;

namespace HackATL_Server.Models.Repository.Services
{
    public class ChatService : IChatService
    {
        private DataContext context;
        public ChatService(DataContext _context)
        {
            context = _context;
        }

        public UserChat_LogList GetChatRoomList(string userId)
        {
            var user_specific = context.User_GroupChatList.SingleOrDefault(x => x.Id == userId);
            return user_specific;
        }

        public ChatRoom_participants AddInitiate(AddChatRoom model)
        {
            ChatRoom_participants chatroom = new ChatRoom_participants();
            UserChatList_Group group = new UserChatList_Group();

            group.RId = model.Rid;
            group.UsersList = model.Users;
            
            chatroom.RId = model.Rid;
            group.RId = model.Rid;
            
            var users = model.Users;
            chatroom.members = model.Users;

            if(users != null)
            {
                foreach (var user in users)
                {
                    AddIndividualsToRoom(user, group);
                    chatroom.members.Add(user);
                    context.SaveChanges();
                }
            }
            
            context.chatRoom_Participants.Add(chatroom);
            context.SaveChanges();
            return context.chatRoom_Participants.Find(model.Rid);
           

        }
        void AddIndividualsToRoom(UserChatList_Component eachUser, UserChatList_Group group)
        {

            var userRoom = context.User_GroupChatList.FirstOrDefault(x => x.Id == eachUser.Uid);
            userRoom.ChatList.Add(group);
            context.User_GroupChatList.Update(userRoom);

        }
    }
}
