using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OTL_API.Entities
{
    public class TaskListContext : DbContext
    {
        public TaskListContext(DbContextOptions<TaskListContext> options) : base(options)
        {
            //Comment below for using add-migration or changing the tables
            Database.Migrate();
        }

        public DbSet<TaskList> TaskLists { get; set; }
    }
}
