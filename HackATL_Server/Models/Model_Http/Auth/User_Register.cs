using System;
using HackATL_Server.Models.Model.MongoDatabase.Users;

namespace HackATL_Server.Models.Model_Http.Auth
{
    public class User_Register
    {
        public string uID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public User_Personal personal { get; set; }
    }
}
