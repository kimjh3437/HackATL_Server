using System;
namespace HackATL_Server.Models.Model
{
    public class Agenda_Item
    {

        public string ID { get; set; }
        public string EventName { get; set; }
        public DateTime Datetime { get; set; }
        public string Day { get; set; }
        public string Location { get; set; }
        public string Cateogory { get; set; }
        public string ShortDescription { get; set; } //Description that appears on the agenda
        public string LongDescription { get; set; }
        public string Speaker_one { get; set; }
        public string Speaker_two { get; set; }
        public string ImageSource { get; set; }
    }
}
