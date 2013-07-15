using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tasky.Models;

namespace tasky.Repository
{
    public interface ITaskRepository
    {
        IEnumerable<Task> FindAll();
        IEnumerable<Task> FindWithFilters(string statusFilter, int? teamMemberFilter);
        int Save(Task s);
        Task FindById(int id);
        void Delete(int id);
    }
}
