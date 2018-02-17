using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OTL_API.Entities;

namespace OTL_API.Services
{
    public class OnlineTaskListRepository : IOnlineTaskListRepository
    {
        private TaskListContext _ctx;

        public OnlineTaskListRepository(TaskListContext ctx)
        {
            _ctx = ctx;
        }

        public bool Save()
        {
            return (_ctx.SaveChanges() >= 0);
        }

        public bool UserIDExist(int userID)
        {
            return _ctx.TaskLists.Any(t => t.UserID == userID);
        }

        public IEnumerable<TaskList> ReadUserTasks(int userID)
        {
            return _ctx.TaskLists
                    .Where(t => t.UserID == userID)
                    .OrderBy(t => t.DateCreated)
                    .ToList();
        }

        public TaskList ReadUserTask(int userID, Guid taskId)
        {
            return _ctx.TaskLists
                    .Where(t => t.UserID == userID && t.TaskListID == taskId)
                    .FirstOrDefault();
        }

        public void CreateTask(TaskList taskList)
        {
            _ctx.TaskLists.Add(taskList);
        }

        public void DeleteTask(TaskList taskList)
        {
            _ctx.TaskLists.Remove(taskList);
        }
    }
}
