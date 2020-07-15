using System;
namespace HackATL_Server.Models.Model.MongoDatabase.Auth
{
    public class AuthUser
    {
        public string uID { get; set; }

        public string Username { get; set; }

        public Byte[] PasswordHash { get; set; }

        public Byte[] PasswordSalt { get; set; }
    }
}
