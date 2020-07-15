using System;
using System.Collections.Generic;

namespace HackATL_Server.Models.Model.MongoDatabase.Agenda
{
    public class Agenda_Detail
    {
        public string Description { get; set; }

        public DateTime Time { get; set; }

        public string Location { get; set; }

        public List<Agenda_Detail_Speaker> Speakers { get; set; }
    }
}
