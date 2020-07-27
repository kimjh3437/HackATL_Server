using System;
using System.Linq;
using System.Threading.Tasks;
using HackATL_Server.Helper;
using HackATL_Server.Models.Model;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Internal;

namespace HackATL_Server
{
    public class ChatHub : Hub
    {

        public ChatHub()
        {
        }


        public async Task AddToGroup(string roomID, string username, string uid)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomID);

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
