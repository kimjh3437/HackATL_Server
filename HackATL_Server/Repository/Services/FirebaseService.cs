using System;
using Firebase.Database;
using Microsoft.Extensions.Configuration;

namespace HackATL_Server.Models.Repository.Services
{
    
    public class FirebaseService
    {
        private readonly IConfiguration _config;

        FirebaseClient fbClient; 
        public FirebaseService(IConfiguration configuration)
            
        {
            _config = configuration;
            fbClient = new FirebaseClient(configuration.GetSection("Firebase:FirebaseURL").Value);
        }
        

        
    }
}
