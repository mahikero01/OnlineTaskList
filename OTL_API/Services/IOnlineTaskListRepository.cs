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

        IEnumerable<TaskList> ReadTasks();

        TaskList ReadTask(Guid taskId);

        void CreateTask(TaskList taskList);

        void DeleteTask(TaskList taskList);
    }
}
