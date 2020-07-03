using System;
using HackATL_Server.Models.Model;
using HackATL_Server.Models.Model.authentication;
using HackATL_Server.Models.Model.Chat_related;
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
        public DbSet<UserPersonalInfo> Users_Personal { get; set; }

        public DbSet<Agenda_Item> AgendaItems { get; set; }


        // chatroom members per user
        public DbSet<UserChat_LogList> User_GroupChatList { get; set; }


        //general and overall chat log 
        public DbSet<UserChat> UserChatLog { get; set; }

        //per room, members 
        public DbSet<ChatRoom_participants> chatRoom_Participants { get; set; }
        public DbSet<PublicModel> User_Public { get; set; }

        //dotnet ef migrations add ChatDataUpdate --context DataContext_Dev --output-dir DevMigrations/SqlServerMigrations
        //dotnet ef migrations add ChatDataUpdate --context DataContext --output-dir Migrations/SqlServerMigrations
        //dotnet ef database update
    }
}
