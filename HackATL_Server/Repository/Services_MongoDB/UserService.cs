﻿using System;
using HackATL_Server.Helper;
using HackATL_Server.Models.Model.MongoDatabase.Users;
using HackATL_Server.Models.MongoDatabase.Settings;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace HackATL_Server.Models.Repository.Services_MongoDB
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;
     

        public UserService(MongoDBSettings _settings)
        {

            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _users = database.GetCollection<User>(_settings.User);

        }
    }
}