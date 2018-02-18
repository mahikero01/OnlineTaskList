using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OTL_API.Entities;

namespace OTL_API.Services
{
    public class OnlineTaskListsRepository : IOnlineTaskListsRepository
    {
        private OnlineTaskListsContext _ctx;

        public OnlineTaskListsRepository(OnlineTaskListsContext ctx)
        {
            _ctx = ctx;
        }

        public bool Save()
        {
            return (_ctx.SaveChanges() >= 0);
        }

        public IEnumerable<UserTask> ReadUserTasks()
        {
            return _ctx.UserTasks.OrderBy(ut => ut.DateCreated).ToList();
        }

        public UserTask ReadUserTask(Guid userTaskID)
        {
            return _ctx.UserTasks.Where(ut => ut.UserTaskID == userTaskID).FirstOrDefault();
        }

        public void CreateUserTask(UserTask userTask)
        {
            _ctx.UserTasks.Add(userTask);
        }

        public void DeleteUserTask(UserTask userTask)
        {
            _ctx.UserTasks.Remove(userTask);
        }
    }
}
