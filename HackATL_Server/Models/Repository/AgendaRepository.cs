using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using HackATL_Server.Models.Model;

namespace HackATL_Server.Models.Repository
{
    public class AgendaRepository : IAgendaRepository
    {
        private static ConcurrentDictionary<string, Agenda_Item> agendaList =
            new ConcurrentDictionary<string, Agenda_Item>();

        public AgendaRepository()
        {
            //add defaults
        }

        public IEnumerable<Agenda_Item> GetAll()
        {
            return agendaList.Values;

        }

        public void Add(Agenda_Item agenda)
        {
            agenda.ID = Guid.NewGuid().ToString();
            agendaList[agenda.ID] = agenda;
        }

        public Agenda_Item Get(string id)
        {
            agendaList.TryGetValue(id, out Agenda_Item agenda);
            return agenda; 
        }

        public Agenda_Item Remove(string id)
        {
            agendaList.TryRemove(id, out Agenda_Item agenda);
            return agenda;
        }

        public void Update(Agenda_Item agenda)
        {
            agendaList[agenda.ID] = agenda;
        }

    }
}
