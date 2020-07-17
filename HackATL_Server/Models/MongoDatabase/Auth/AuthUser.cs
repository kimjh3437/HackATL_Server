using System;
namespace HackATL_Server.Models.Model.MongoDatabase.Auth
{
    public class AuthUser
    {
        public string uID { get; set; }

        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
}
