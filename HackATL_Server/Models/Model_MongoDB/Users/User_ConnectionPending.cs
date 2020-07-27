using System;
namespace HackATL_Server.Models.Model_MongoDB.Users
{
    public class User_ConnectionPending
    {
        public string uID { get; set; } //sender or receiver 

        public string Stance { get; set; } // whether the request is sent or received

        public Boolean Status { get; set; }
        

        // public User_Personal Personal {get;set;} ? maybe 
    }

    public static class SentOrReceived
    {
        public static string Sent { get; set; } = "Sent";

        public static string Received { get; set; } = "Received";
    }
}
