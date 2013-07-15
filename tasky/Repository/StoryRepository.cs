using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using tasky.DAL;
using tasky.Models;

namespace tasky.Repository
{
    public class StoryRepository : IStoryRepository
    {
        private TaskyContext db = new TaskyContext();

        public IEnumerable<Story> FindAll()
        {
            return db.Stories.ToList();
        }

        public IEnumerable<Story> FindWithFilters(string statusFilter, int? sprintFilter)
        {
            var storyQuery = db.Stories.AsQueryable();
            if (statusFilter.Length > 0)
            {
                storyQuery = storyQuery.Where(model => model.status == statusFilter);
            }
            if (sprintFilter != null)
            {
                storyQuery = storyQuery.Where(model => model.sprint.id == (int)sprintFilter);
            }
            return storyQuery.ToList();
        }

        public IEnumerable<Task> FindTasksForStory(int id)
        {
            return db.Tasks.Where(model => model.storyId == id).ToList();
        }

        public int Save(Story s)
        {
            if (s.id > 0)
            {
                db.Entry(s).State = EntityState.Modified;
            }
            else
            {
                s = db.Stories.Add(s);
            }
            db.SaveChanges();
            return s.id;
        }

        public Story FindById(int id)
        {
            return db.Stories.Find(id);
        }

        public void Delete(int id)
        {
            Story s = this.FindById(id);
            db.Stories.Remove(s);
            db.SaveChanges();
        }
    }
}