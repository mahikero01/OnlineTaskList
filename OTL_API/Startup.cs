using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using OTL_API.Entities;
using OTL_API.Services;

namespace OTL_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set;  }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var a = Startup.Configuration["JWT:ValidIssuer"];
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Startup.Configuration["JWT:ValidIssuer"],
                    ValidAudience = Startup.Configuration["JWT:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Startup.Configuration["JWT:IssuerSigningKey"])),
                };
            });

            services.AddCors(
                    opt =>
                    {
                        opt.AddPolicy("AllowWebClient", c => c.WithOrigins("http://localhost:50282/"));
                    });

            services.AddMvc();

            //this will make JSON as statement case
            services.AddMvc()
                    .AddJsonOptions(o =>
                    {
                        if (o.SerializerSettings.ContractResolver != null)
                        {
                            var castedResolver = o.SerializerSettings.ContractResolver
                                    as DefaultContractResolver;
                            castedResolver.NamingStrategy = null;
                        }
                    });

            //Use for migration only, then comment all statement in DB context constructor
            //var connectionString = "Server=(localdb)\\mssqllocaldb; Database=TaskListDB; trusted_Connection=True;";
            //Use below in Production
            var connectionString = Startup.Configuration["ConnectionStrings:TaskListDBConnectionString"];

            services.AddDbContext<OnlineTaskListsContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped <IOnlineTaskListsRepository, OnlineTaskListsRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
                IApplicationBuilder app, 
                IHostingEnvironment env, 
                OnlineTaskListsContext taskListContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //this is used for autherization
            app.UseAuthentication();

            //This is for Seeding comment this when ading migration, comment this out when creating new migration
            taskListContext.EnsureSeedDataForContext();

            AutoMapper.Mapper.Initialize(
                    cfg =>
                    {
                        cfg.CreateMap<Models.UserTaskForCreateDTO, Entities.UserTask>()
                                .ForMember(dest => dest.UserTaskID, opt => opt.MapFrom(o => new Guid()))
                                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(o => DateTime.Now))
                                .ForMember(dest => dest.IsDone, opt => opt.MapFrom(o => false));
                        cfg.CreateMap<Models.UserTaskForUpdateDTO, Entities.UserTask>();
                    });

            app.UseMvc();
        }
    }
}
