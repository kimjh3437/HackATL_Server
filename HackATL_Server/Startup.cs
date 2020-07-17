using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HackATL_Server.Helper;
using HackATL_Server.Models.Model;
using HackATL_Server.Models.MongoDatabase.Settings;
using HackATL_Server.Models.Repository;
using HackATL_Server.Models.Repository.Interfaces;
using HackATL_Server.Models.Repository.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using HackATL_Server.Models.Repository.Services_MongoDB;
using UserService = HackATL_Server.Models.Repository.UserService;
using HackATL_Server.Repository.Interfaces_MongoDB;
using HackATL_Server.Repository.Services_MongoDB;

namespace HackATL_Server
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _env = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //if (_env.IsProduction())
            //    services.AddDbContext<DataContext>();
            //services.AddDbContext<DataContext_Dev>(options => options.UseSqlServer(Configuration.GetConnectionString("DevDatabase")));
            //services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("NewDatabase")));

          
            services.AddAutoMapper(typeof(Startup));


            services.AddControllers();
            
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            //mongo
            services.AddSingleton<IUserService_md, UserService_md>();
            services.AddSingleton<IChatService_md, ChatService_md>();


            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAgendaService, AgendaService>();

            //added manually

            // requires using Microsoft.Extensions.Options
            services.Configure<MongoDBSettings>(
                Configuration.GetSection("MongoDBSettings"));

            //services.AddScoped<IMongoDBSettings>(sp =>
            //    sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);

            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IAgendaRepository, AgendaRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IChatService, ChatService>();

            //services.AddSignalR().AddAzureSignalR();

            services.AddSignalR(hubOptions =>
            {
                hubOptions.EnableDetailedErrors = true;
            }).AddAzureSignalR(Configuration.GetSection("Config:AzureSignalRConnectionString").Value);



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chat/hub");
            });
        }
    }
}
