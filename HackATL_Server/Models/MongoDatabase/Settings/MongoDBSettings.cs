using System;
using HackATL_Server.Models.Model;
using HackATL_Server.Models.Model.MongoDatabase.Agenda;
using HackATL_Server.Models.Model.MongoDatabase.Chats;
using HackATL_Server.Models.Model.MongoDatabase.Forums;
using HackATL_Server.Models.Model.MongoDatabase.Reservations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HackATL_Server.Models.MongoDatabase.Settings
{
    public class MongoDBSettings : IMongoDBSettings 
    {
        
        public string User { get; set; }

        public string Agenda { get; set; }

        public string Thread { get; set; }

        public string Chatroom { get; set; }

        public string Reservation { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }

    public interface IMongoDBSettings
    {
        string User { get; set; }

        string Agenda { get; set; }

        string Thread { get; set; }

        string Chatroom { get; set; }

        string Reservation { get; set; }

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }
    }



}
