using System;
using System.Linq;
using System.Threading.Tasks;
using HackATL_Server.Helper;
using HackATL_Server.Models.Model;
using HackATL_Server.Models.Model.Chat_related;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Internal;

namespace HackATL_Server
{
    public class ChatHub : Hub
    {
        DataContext _context;
        UserChatList_Component component;
        public ChatHub(DataContext context)
        {
            _context = context;
            component = new UserChatList_Component();
            

        }
        

        public async Task AddToGroup(string roomID, string username, string uid)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomID);


            component.Uid = uid;
            component.Username = username;


            var list = _context.chatRoom_Participants.Find(roomID);
            if (list == null)
            {
                var newRoom = new ChatRoom_participants
                {
                    RId = roomID,
                    members = { component }
                };
                _context.chatRoom_Participants.Add(newRoom);
                _context.SaveChanges();
            }
            list.members.Add(component);
            var _list = list.members;
            _context.chatRoom_Participants.Update(list);



            var group = _context.User_GroupChatList.Find(uid);
            group.ChatList.FirstOrDefault(x => x.RId == roomID).UsersList = _list;
            _context.User_GroupChatList.Update(group);

            _context.SaveChanges();



            await Clients.Group(roomID).SendAsync("Entered", username, uid);
        }

        public async Task RemoveFromGroup(string groupName, string user)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Left", user);
        }

        public async Task SendMessageGroup(string groupName, string username, string uid, string message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", username, uid, message);
        }
    }
}
