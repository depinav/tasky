using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tasky.Models;

namespace tasky.Repository
{
    public interface ITaskRepository
    {
        ICollection<Task> FindAll();
        ICollection<Task> FindWithFilters(string statusFilter, int? teamMemberFilter);
        int Save(Task s);
        Task FindById(int id);
        void Delete(int id);
        void Log(TaskLog log);
        ICollection<TaskLog> GetLogs();
        TaskLog GetLogById(int id);
    }
}
