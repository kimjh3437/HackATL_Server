using System;
using System.ComponentModel.DataAnnotations;

namespace HackATL_Server.Models.Model.authentication
{
    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
