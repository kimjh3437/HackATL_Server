using System;
using HackATL_Server.Models.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HackATL_Server.Helper
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(Configuration.GetConnectionString("NewDatabase"));
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Agenda_Item> AgendaItems { get; set; }

        


    }
}
