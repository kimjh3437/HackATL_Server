using System;
using HackATL_Server.Models.Model.MongoDatabase.Users;

namespace HackATL_Server.Models.Model_Http.User
{
    public class User_Register // User to register user 
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public User_Personal Personal { get; set; }
    }
}
