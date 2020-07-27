using System;
using System.Collections.Generic;
using HackATL_Server.Models.Model.MongoDatabase.Agenda;
using HackATL_Server.Models.Model_Http.Agenda;
using HackATL_Server.Models.MongoDatabase.Settings;
using HackATL_Server.Repos.Interface;
using HackATL_Server.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HackATL_Server.Repos.Service
{
    public class AgendaService : IAgendaService
    {
        IMongoCollection<Agenda> _agendas;
        IOptions<MongoDBSettings> _settings;
        public AgendaService(
            IOptions<MongoDBSettings> settings)
        {
            _settings = settings;
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(Hackathon.Emory_HackATL);
            _agendas = database.GetCollection<Agenda>(settings.Value.Agenda);
               
        }

        public List<Agenda> GetAgenda_All()
        {
            List<Agenda> agendas = _agendas.Find<Agenda>(x => true).ToList();
            if (agendas == null)
                return null;
            return agendas;
        }

        public Agenda GetAgenda(string agendaID)
        {
            Agenda agenda = new Agenda();
            agenda = _agendas.Find<Agenda>(x => x.agendaID == agendaID).FirstOrDefault();
            if (agenda == null)
                return null;
            return agenda; 
        }

        public Agenda CreateAgenda(Agenda_Create create)
        {
            Agenda agenda = new Agenda();
            Agenda_Detail detail = new Agenda_Detail();
            if(create.Details != null)
                detail = create.Details;
            agenda.Detail = detail;
            agenda.Title = create.Title;
            var agendaID = Guid.NewGuid().ToString();
            agenda.agendaID = agendaID;
            _agendas.InsertOne(agenda);
            return agenda;
        }

        public Boolean DeleteAgenda(string agendaID)
        {
            var filter = Builders<Agenda>.Filter.Eq(x => x.agendaID, agendaID);
            _agendas.DeleteOne(filter);
            return true; 
        }

        public Boolean EditAgenda(Agenda update)
        {
            var agenda = _agendas.Find<Agenda>(x => x.agendaID == update.agendaID).FirstOrDefault();
            if(agenda == null)
            {
                _agendas.InsertOne(update);
                return true; 
            }

            UpdateResult x = null, y = null;
            var filter = Builders<Agenda>.Filter.Eq(x => x.agendaID, update.agendaID);
            if(update.Title != agenda.Title)
            {
                var edit_title = Builders<Agenda>.Update.Set(x => x.Title, update.Title);
                x = _agendas.UpdateOne(filter, edit_title);
            }
            if(update.Detail != agenda.Detail)
            {
                var edit_detail = Builders<Agenda>.Update.Set(x => x.Detail, update.Detail);
                y = _agendas.UpdateOne(filter, edit_detail);
            }
            if( x != null || y != null)
            {
                return true;

            }
            return false;
           
            
        }
    }
}
