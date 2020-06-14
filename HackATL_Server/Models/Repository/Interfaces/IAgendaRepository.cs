using System;
using System.Collections.Generic;
using HackATL_Server.Models.Model;

namespace HackATL_Server.Models.Repository
{
    public interface IAgendaRepository
    {
        void Add(Agenda_Item agenda);
        void Update(Agenda_Item agenda);
        Agenda_Item Remove(string id);
        Agenda_Item Get(string id);
        IEnumerable<Agenda_Item> GetAll();
    }
}
