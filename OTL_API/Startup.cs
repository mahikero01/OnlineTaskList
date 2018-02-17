using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
            services.AddMvc();

            //Use for migration only, then comment all statement in DB context constructor
            //var connectionString = "Server=(localdb)\\mssqllocaldb; Database=TaskListDB; trusted_Connection=True;";
            //Use below in Production
            var connectionString = Startup.Configuration["ConnectionStrings:TaskListDBConnectionString"];

            services.AddDbContext<TaskListContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped <IOnlineTaskListRepository, OnlineTaskListRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, TaskListContext taskListContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //This is for Seeding comment this when ading migration, comment this out when creating new migration
            taskListContext.EnsureSeedDataForContext();

            AutoMapper.Mapper.Initialize(
                    cfg =>
                    {
                        cfg.CreateMap<Entities.TaskList, Models.TaskListDTO>();
                    });

                        app.UseMvc();
        }
    }
}
