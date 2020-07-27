using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HackATL_Server.Models.Model.MongoDatabase.Chats;
using HackATL_Server.Models.Model.MongoDatabase.Users;
using HackATL_Server.Models.Model_Http.Chat;
using HackATL_Server.Models.MongoDatabase.Settings;
using HackATL_Server.Repos.Interface;
using HackATL_Server.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HackATL_Server.Repos.Service
{
    public class ChatService : IChatService
    {
        IMongoCollection<User> _users;
        IMongoCollection<Chatroom> _chatrooms;
        IOptions<MongoDBSettings> _settings;

        public ChatService(
            IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(Hackathon.Emory_HackATL);
            _chatrooms = database.GetCollection<Chatroom>(settings.Value.ConnectionString);
            _users = database.GetCollection<User>(settings.Value.User);
        }

        public Chatroom CreateChat(Chat_Create create)
        {
            Chatroom chat = new Chatroom();
            chat.chatroomID = create.chatroomID;
            List<string> members = create.Participants;
            chat.Participants = members;
            foreach(var id in members)
            {
                var user = _users.Find<User>(x => x.uID == id).FirstOrDefault();
                if (user == null)
                    continue;
                var filter = Builders<User>.Filter.Eq(x => x.uID, id);
                var update = Builders<User>.Update.AddToSet(x => x.Chatrooms, create.chatroomID);
                _users.UpdateOne(filter, update);
            }
            return chat; 

            //------------------------------------------------------------------------------
            //
            // Add notification to all the subscribers and members that initiate this method  
            //
            //------------------------------------------------------------------------------

        }

        

        public Chatroom GetChat(string chatroomID)
        {
            Chatroom chat = new Chatroom();
            chat = _chatrooms.Find<Chatroom>(x => x.chatroomID == chatroomID).FirstOrDefault();
            if (chat == null)
                return null;
            return chat; 
        }

        public async Task<Chat_MemberInfo> AddMembers(Chat_AddMembers add)
        {
            List<string> members = new List<string>();
            members = add.Members;
            foreach(var member in members)
            {
                var filter = Builders<User>.Filter.Eq(x => x.uID, add.uID);
                var update = Builders<User>.Update.AddToSet(x => x.Chatrooms, add.chatroomID);
                _users.UpdateOne(filter, update);
            }

            Chatroom chat = new Chatroom();
            chat = _chatrooms.Find<Chatroom>(x => x.chatroomID == add.chatroomID).FirstOrDefault();
            var filter_chat = Builders<Chatroom>.Filter.Eq(x => x.chatroomID, add.chatroomID);
            var update_chat = Builders<Chatroom>.Update.AddToSet(x => x.Participants, add.uID);
            var status = await _chatrooms.UpdateOneAsync(filter_chat, update_chat);


            Chat_MemberInfo info = new Chat_MemberInfo();
            info = GetMembersInfo_Single(add.chatroomID);
            return info; 
        }

        public Chat_MemberInfo GetMembersInfo_Single(string chatroomID)
        {
            Chatroom chat = new Chatroom();
            Chat_MemberInfo list = new Chat_MemberInfo();
            chat = _chatrooms.Find<Chatroom>(x => x.chatroomID == chatroomID).FirstOrDefault();
            List<User_Personal> Infos = new List<User_Personal>();
            foreach(var id in chat.Participants)
            {
                var user = _users.Find<User>(x => x.uID == id).FirstOrDefault();
                if (user == null)
                    continue;
                Infos.Add(user.Personal);
            }
            list.MemberInfos = Infos;
            return list;

        }

        public List<Chat_MemberInfo> GetMembersInfo_All(List<string> chatroomIDs)
        {
            List<Chat_MemberInfo> list = new List<Chat_MemberInfo>();
            foreach(var chatroomID in chatroomIDs)
            {
                Chat_MemberInfo info = new Chat_MemberInfo();
                info = GetMembersInfo_Single(chatroomID);
                if (info == null)
                    continue;
                list.Add(info);
            }
            return list; 
        }

        public Boolean LeaveChat(Chat_Leave leave)
        {
            var filter_chat = Builders<Chatroom>.Filter.Eq(x => x.chatroomID, leave.chatroomID);
            var update_chat = Builders<Chatroom>.Update.Pull(x => x.Participants, leave.uID);
            var status = _chatrooms.UpdateOne(filter_chat, update_chat);

            var filter_user = Builders<User>.Filter.Eq(x => x.uID, leave.uID);
            var update_user = Builders<User>.Update.Pull(x => x.Chatrooms, leave.chatroomID);
            var status1 = _users.UpdateOne(filter_user, update_user);
            if(status != null && status1 != null)
            {
                return true;
            }
            return false;
        }

        
       

        
    }
}
