using OTL_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OTL_API.Services
{
    public interface IOnlineTaskListRepository
    {
        bool Save();

        bool UserIDExist(int userID);

        IEnumerable<TaskList> ReadUserTasks(int userID);

        TaskList ReadUserTask(int userID, Guid taskId);

        void CreateTask(TaskList taskList);

        void DeleteTask(TaskList taskList);
    }
}
