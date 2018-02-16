using OTL_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OTL_API
{
    public static class TaskListExtension
    {
        public static void EnsureSeedDataForContext(this TaskListContext ctx)
        {
            SeedTaskLists(ctx);
        }
        private static void SeedTaskLists(TaskListContext ctx)
        {
            if (ctx.TaskLists.Any())
            {
                return;
            }

            var taskLists = new List<TaskList>()
                    {
                        new TaskList()
                        {
                            TaskListID = new Guid(),
                            UserID = 1,
                            Title = "Initial",
                            Description = "sample Description",
                            DateCreated = DateTime.Now,
                            IsDone = true
                        }
                    };

            ctx.TaskLists.AddRange(taskLists);
            ctx.SaveChanges();
        }
    }
}
