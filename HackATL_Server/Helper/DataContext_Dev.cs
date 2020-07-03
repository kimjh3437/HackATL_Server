using System;
using HackATL_Server.Models.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HackATL_Server.Helper
{
    public class DataContext_Dev : DbContext
    {
        protected readonly IConfiguration configuration; 
        public DataContext_Dev(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(configuration.GetConnectionString("DevDatabase"));
        }
        public DbSet<User> Users { get; set; } //user data

        
    }
}
//dotnet ef database update
//dotnet ef migrations add ChatDataUpdate --context DataContext_Dev --output-dir DevMigrations/SqlServerMigrations