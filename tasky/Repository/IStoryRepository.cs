using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tasky.Models;

namespace tasky.Repository
{
    public interface IStoryRepository
    {
        ICollection<Story> FindAll();
        ICollection<Story> FindWithFilters(string statusFilter, int? sprintFilter);
        ICollection<Task> FindTasksForStory(int id);
        int Save(Story s);
        Story FindById(int id);
        void Delete(int id);
    }
}
