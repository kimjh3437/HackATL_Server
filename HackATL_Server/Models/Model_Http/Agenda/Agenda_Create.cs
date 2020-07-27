using System;
using HackATL_Server.Models.Model.MongoDatabase.Agenda;
using HackATL_Server.Models.Model_General;

namespace HackATL_Server.Models.Model_Http.Agenda
{
    public class Agenda_Create
    {
        public string Title { get; set; }

        public Agenda_Detail Details { get; set; }
     
    }
}
