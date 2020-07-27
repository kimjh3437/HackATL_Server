using System;
using System.Collections.Generic;
using HackATL_Server.Models.Model.MongoDatabase.Agenda;
using HackATL_Server.Models.Model_Http.Agenda;

namespace HackATL_Server.Repos.Interface
{
    public interface IAgendaService
    {
        List<Agenda> GetAgenda_All();

        Agenda GetAgenda(string agendaID);

        Agenda CreateAgenda(Agenda_Create create);

        Boolean DeleteAgenda(string agendaID);

        Boolean EditAgenda(Agenda update);




    }
}
