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
        //Settings
        public string ConnectionString { get; set; }

        //Database
        public string DatabaseName { get; set; }

        public string Hackathon { get; set; }

        //Collections 
        public string User { get; set; }

        public string Agenda { get; set; }

        public string Thread { get; set; }

        public string Chatroom { get; set; }

        public string Reservation { get; set; }

        public string AuthUser { get; set; }

      

    }

    public interface IMongoDBSettings
    {
        //Settings
        public string ConnectionString { get; set; }

        //Database
        public string Hackathon { get; set; }

        public string DatabaseName { get; set; }

        //Collections 
        public string User { get; set; }

        public string Agenda { get; set; }

        public string Thread { get; set; }

        public string Chatroom { get; set; }

        public string Reservation { get; set; }

        public string AuthUser { get; set; }
    }



}
