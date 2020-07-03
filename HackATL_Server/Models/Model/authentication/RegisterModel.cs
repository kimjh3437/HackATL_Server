using System;
using System.ComponentModel.DataAnnotations;

namespace HackATL_Server.Models.Model.authentication
{
    public class RegisterModel
    {
        //[Required]
        public string FirstName { get; set; }

        //[Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        //[Required]
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
