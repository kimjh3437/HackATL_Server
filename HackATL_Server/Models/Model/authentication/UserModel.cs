using System;
namespace HackATL_Server.Models.Model.authentication
{
    public class UserModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }

        //new
        public string Major { get; set; }
        public string University { get; set; }
        public string LinkedUsername { get; set; }
        public string FacebookUsername { get; set; }
        public string InstagramUsername { get; set; }
        public string TwitterUsername { get; set; }
        public string AdditionalInfo { get; set; }
        public string Status { get; set; }


    }
}
