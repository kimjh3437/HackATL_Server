﻿using System;
using System.Collections.Generic;
using HackATL_Server.Models.Model;
using HackATL_Server.Models.Model.authentication;

namespace HackATL_Server.Models.Repository
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(string id);
        User Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(string id);
        bool Check(string username);
    }
}