using System;
using System.Collections.Generic;
using HackATL_Server.Models.Model.MongoDatabase.Chats;
using HackATL_Server.Models.Model.MongoDatabase.Users;
using HackATL_Server.Models.MongoDatabase.Settings;
using HackATL_Server.Repository.Interfaces_MongoDB;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HackATL_Server.Repository.Services_MongoDB
{
    public class ChatService_md : IChatService_md
    {
        private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<Chatroom> _chatrooms;
        public ChatService_md(IOptions<MongoDBSettings> _settings)
        {
            var client = new MongoClient(_settings.Value.ConnectionString);
            var database = client.GetDatabase(_settings.Value.DatabaseName);

            _users = database.GetCollection<User>(_settings.Value.User);
            _chatrooms = database.GetCollection<Chatroom>(_settings.Value.Chatroom);
        }

        public List<User_Personal> GetMembers(List<string> membersID)
        {
            List<User_Personal> personals = new List<User_Personal>();
            foreach (var uID in membersID)
            {
                var user = _users.Find<User>(x => x.uID == uID).FirstOrDefault();
                personals.Add(user.Personal);
            }
            return personals; 



        }
        public List<Chatroom> GetChatrooms(string uID)
        {
            var user = _users.Find<User>(x => x.uID == uID).FirstOrDefault();
            if(user == null)
            {
                return null;
            }
            List<Chatroom> rooms = new List<Chatroom>();
            foreach(var x in user.Chatrooms)
            {
                var room = _chatrooms.Find<Chatroom>(y => y.chatID == x).FirstOrDefault();
                rooms.Add(room);
            }
            return rooms;  
        }
        public List<User_Personal> CreateChatroom(List<string> membersID)
        {
            List<User_Personal> personals = new List<User_Personal>();
            var chatID = Guid.NewGuid().ToString();
            Chatroom chatroom = new Chatroom();
            chatroom.chatID = chatID;
            chatroom.Participants = membersID; 
            
            foreach(var x in membersID)
            {
                var user = _users.Find<User>(z => z.uID == x).FirstOrDefault();
                user.Chatrooms.Add(chatID);

                personals.Add(user.Personal);
                var status = Update(x, user);     
            }
            return personals; 
        }

        public Boolean AddMember(string uID, string chatID)
        {
            var chatroom = _chatrooms.Find(x => x.chatID == chatID).FirstOrDefault();
            var filter = Builders<Chatroom>.Filter.Eq(x => x.chatID, chatID);
            var update = Builders<Chatroom>.Update.AddToSet(x => x.Participants, uID);
            var x = _chatrooms.UpdateOne(filter, update);
            if (x != null)
            {
                return true;
            }
            else return false; 
        }

        public Boolean DeleteChatroom(string uID, string chatID)
        {
            _chatrooms.DeleteOne(x => x.chatID == chatID);
            var user = _users.Find<User>(x => x.uID == uID).FirstOrDefault();
            user.Chatrooms.Remove(chatID);
            var z = _users.ReplaceOne<User>(x => x.uID == uID, user);
            if (z == null)
                return false;
            return true; 
        }
        bool Update(string uID, User user)
        {
            var x = _users.ReplaceOne<User>(x => x.uID == uID, user);
            if (x != null)
                return true;
            return false; 
        }

        public void hi()
        {

        }
    }
}
