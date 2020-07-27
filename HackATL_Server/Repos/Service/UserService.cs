using System;
using System.Collections.Generic;
using HackATL_Server.Models.Model.MongoDatabase.Users;
using HackATL_Server.Models.Model_Http.User;
using HackATL_Server.Models.Model_MongoDB.Users;
using HackATL_Server.Models.MongoDatabase.Settings;
using HackATL_Server.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HackATL_Server.Repos.Service
{
    public class UserService
    {
        IMongoCollection<User> _users;
        public UserService(IOptions<MongoDBSettings>settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(Hackathon.Emory_HackATL);
            _users = database.GetCollection<User>(settings.Value.User);
        }

        public Boolean ConnectionRequest(User_ConnectionRequest connection)
        {
            List<User> users = new List<User>();
            users = _users.Find<User>(x => x.uID == connection.uID || x.uID == connection.connectionID).ToList();
            foreach(var user in users)
            {
                var filter = Builders<User>.Filter.Eq(x => x.uID, user.uID);
                if (user.uID == connection.uID)
                {
                    //user with connection.uID is sending the request 
                    User_ConnectionPending pending = new User_ConnectionPending();
                    pending.Stance = SentOrReceived.Sent;
                    pending.uID = connection.connectionID;

                    var update = Builders<User>.Update.AddToSet(x => x.PendingConnection, pending);
                    _users.UpdateOne(filter, update);

                }
                else
                {
                    User_ConnectionPending pending = new User_ConnectionPending();
                    pending.Stance = SentOrReceived.Received;
                    pending.uID = connection.uID;

                    var update = Builders<User>.Update.AddToSet(x => x.PendingConnection, pending);
                    _users.UpdateOne(filter, update);

                }
            }
            return true;

        }

        public Boolean ConnectionResponse(User_ConnectionResponse pending)
        {
            List<User> users = new List<User>();
            users = _users.Find<User>(x => x.uID == pending.uID || x.uID == pending.connectorID).ToList();
            User_ConnectionResponse _pending = new User_ConnectionResponse();
            _pending = pending;
            if (_pending.Status)
            {
                foreach(var user in users)
                {
                    var filter = Builders<User>.Filter.Eq(x => x.uID, user.uID);
                    if(user.uID == pending.uID)
                    {
                        //one that received 
                        var update_0 = Builders<User>.Update.AddToSet(x => x.Connections, pending.connectorID);
                        _users.UpdateOne(filter, update_0);
                        User_ConnectionPending request = new User_ConnectionPending();
                        request.uID = pending.connectorID;
                        request.Stance = SentOrReceived.Received;

                        var update_1 = Builders<User>.Update.Pull(x => x.PendingConnection, request);
                        _users.UpdateOne(filter, update_1);
                        
                    }
                    else
                    {
                        var update = Builders<User>.Update.AddToSet(x => x.Connections, pending.uID);
                        _users.UpdateOne(filter, update);
                        User_ConnectionPending request = new User_ConnectionPending();
                        request.uID = pending.uID;
                        request.Stance = SentOrReceived.Sent;
                        var update1 = Builders<User>.Update.Pull(x => x.PendingConnection, request);
                        _users.UpdateOne(filter, update1);
                    }
                }
                
            }
            return true;
        }

        public Boolean DeleteConnection()
        {
            return true; 

        }
    }
}
