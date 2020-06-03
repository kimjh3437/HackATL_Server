using System;
using System.ComponentModel.DataAnnotations;

namespace HackATL_Server.Models.Model
{
    public class Item
    {
        public string Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
