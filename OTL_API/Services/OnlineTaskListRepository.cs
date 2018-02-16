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
            throw new NotImplementedException();
        }

        public void CreateTask(TaskList taskList)
        {
            throw new NotImplementedException();
        }

        public TaskList ReadTask(Guid taskListId)
        {
            return _ctx.TaskLists.Where(t => t.TaskListID == taskListId).FirstOrDefault();
        }

        public IEnumerable<TaskList> ReadTasks()
        {
            return _ctx.TaskLists.OrderBy(t => t.DateCreated).ToList();
        }

        public void DeleteTask(TaskList taskList)
        {
            throw new NotImplementedException();
        }
    }
}
