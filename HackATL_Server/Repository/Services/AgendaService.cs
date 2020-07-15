using System;
using System.Collections.Generic;
using HackATL_Server.Helper;
using HackATL_Server.Models.Model;
using HackATL_Server.Models.Repository.Interfaces;

namespace HackATL_Server.Models.Repository
{
    public class AgendaService : IAgendaService
    {
        private DataContext _context;
        public AgendaService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Agenda_Item> GetAllEvents()
        {
            return _context.AgendaItems;
        }

        public Agenda_Item GetEvent(string id)
        {
            return _context.AgendaItems.Find(id);
        }
        public Agenda_Item CreateAgenda(Agenda_Item AgendaEvent) {
            // needs mofication 
            _context.Add(AgendaEvent);
            _context.SaveChanges();
            return AgendaEvent;

        }
        public void UpdateEvent(Agenda_Item UpdatedEvent)
        {
        
            var model = _context.AgendaItems.Find(UpdatedEvent.ID);
            _context.AgendaItems.Update(model);
            _context.SaveChanges();
        }
        public Agenda_Item DeleteEvent(string id)
        {
            var model = _context.AgendaItems.Find(id);
            if(model != null)
            {
                _context.AgendaItems.Remove(model);
                _context.SaveChanges();
                return model;
            }
            return null;

        }
    }
}
