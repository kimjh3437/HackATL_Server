using System;
using System.Collections.Generic;
using HackATL_Server.Models.Model.authentication;
using HackATL_Server.Models.Model.MongoDatabase.Auth;
using HackATL_Server.Models.Model.MongoDatabase.Users;
using HackATL_Server.Models.Model_Http.Auth;
using HackATL_Server.Models.MongoDatabase.Settings;

namespace HackATL_Server.Repository.Interfaces_MongoDB
{
    public interface IUserService_md
    {
        User Authenticate(string username, string password);

        User Register(User_Register registerModel);

        bool Check(string username);

        IEnumerable<User> GetAll();

        User GetUser(string uID);

        void UpdateUser(string uID, User_Personal personal);

        void Delete(string uID);

        IEnumerable<User_Personal> GetAll_Public();







    }
}
