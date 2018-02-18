using OTL_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OTL_API.Services
{
    public interface IOnlineTaskListsRepository
    {
        bool Save();

        IEnumerable<UserTask> ReadUserTasks();

        UserTask ReadUserTask(Guid userTaskID);

        void CreateUserTask(UserTask userTask);

        void DeleteUserTask(UserTask userTask);
    }
}
