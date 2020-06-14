using System;
using System.Collections.Generic;
using HackATL_Server.Models.Model;

namespace HackATL_Server.Models.Repository.Interfaces
{
    public interface IAgendaService
    {
        IEnumerable<Agenda_Item> GetAllEvents();
        Agenda_Item GetEvent(string id);
        Agenda_Item CreateAgenda(Agenda_Item AgendaEvents);
        void UpdateEvent(Agenda_Item UpdatedEvent);
        Agenda_Item DeleteEvent(string id);
    }
}
