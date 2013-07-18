using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using tasky.DAL;
using tasky.Models;

namespace tasky.Repository
{
    public class SprintRepository : ISprintRepository
    {
        private TaskyContext db = new TaskyContext();

        public ICollection<Sprint> FindAll()
        {
            return db.Sprints.ToList();
        }

        public ICollection<Story> FindStoriesForSprint(int id)
        {
            return db.Stories
                .Where(model => model.sprintId == id)
                .OrderBy(story => story.sprintOrder)
                .ToList();
        }

        public int Save(Sprint s)
        {
            if (s.id > 0)
            {
                db.Entry(s).State = EntityState.Modified;
            }
            else
            {
                s = db.Sprints.Add(s);
            }
            db.SaveChanges();
            return s.id;
        }

        public Sprint FindById(int id)
        {
            Sprint result = db.Sprints.Find(id);
            if (result != null)
            {
                result.stories = db.Stories.Where(s => s.sprintId == id).OrderBy(s => s.title).ToList();
            }
            return result;
        }

        public void Delete(int id)
        {
            Sprint sprint = this.FindById(id);
            db.Sprints.Remove(sprint);
            db.SaveChanges();
        }
    }
}