using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tasky.Models;

namespace tasky.Repository
{
    public interface IStoryRepository
    {
        IEnumerable<Story> FindAll();
        IEnumerable<Story> FindWithFilters(string statusFilter, int? sprintFilter);
        IEnumerable<Task> FindTasksForStory(int id);
        int Save(Story s);
        Story FindById(int id);
        void Delete(int id);
    }
}
