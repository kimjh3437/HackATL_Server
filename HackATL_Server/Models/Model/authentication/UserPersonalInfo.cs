using System;
using System.ComponentModel.DataAnnotations;

namespace HackATL_Server.Models.Model.authentication
{
    public class UserPersonalInfo
    {
        [Key]
        public string Uid { get; set; }
        public string University { get; set; }
        public string LinkedUsername { get; set; }
        public string FacebookUsername { get; set; }
        public string InstagramUsername { get; set; }
        public string TwitterUsername { get; set; }
    }
}
