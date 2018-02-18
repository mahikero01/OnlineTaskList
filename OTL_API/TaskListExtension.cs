using OTL_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OTL_API
{
    public static class OnlineTaskListsExtension
    {
        public static void EnsureSeedDataForContext(this OnlineTaskListsContext ctx)
        {
            SeedTaskLists(ctx);
        }
        private static void SeedTaskLists(OnlineTaskListsContext ctx)
        {
            if (ctx.UserTasks.Any())
            {
                return;
            }

            var userTaskLists = new List<UserTask>()
                    {
                        new UserTask()
                        {
                            UserTaskID = new Guid(),
                            UserID = "3142b052-f32a-408a-ace9-f6a556e92c1b",
                            Title = "Initial",
                            Description = "sample Description",
                            DateCreated = DateTime.Now,
                            IsDone = true
                        },
                        new UserTask()
                        {
                            UserTaskID = new Guid(),
                            UserID = "3142b052-f32a-408a-ace9-f6a556e92c1b",
                            Title = "Secondary",
                            Description = "sample 2 Description",
                            DateCreated = (DateTime.Now).AddDays(1),
                            IsDone = true
                        }
                    };

            ctx.UserTasks.AddRange(userTaskLists);
            ctx.SaveChanges();
        }
    }
}
