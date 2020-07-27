using System;
using System.Collections.Generic;
using HackATL_Server.Models.Model_General;

namespace HackATL_Server.Models.Model.MongoDatabase.Agenda
{
    public class Agenda_Detail
    {
        
        public string Category { get; set; }

        public string Description { get; set; }

        public EventTime Time { get; set; }

        public string Day { get; set; } // maybe this needs to be DateTime?

        public string Location { get; set; }

        public List<Agenda_Detail_Speaker> Speakers { get; set; }
    }
}
