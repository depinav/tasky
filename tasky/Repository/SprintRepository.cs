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

        public ICollection<TaskLog> FindTaskLogsForSprint(int id)
        {
            return db.TaskLogs
                .Where(model => model.task.story.sprintId == id)
                .OrderBy(model => model.logDate)
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

        public int SumTaskEstimatesForSprint(int id)
        {
            //we have to do a task count beforehand because mvc raises an error for some reason
            //instead of just returning 0 if there are no records
            if (db.Tasks.Where(model => model.story.sprintId == id).Count() > 0)
            {
                return db.Tasks.Where(model => model.story.sprintId == id).Sum(model => model.Estimate_Hours);
            }
            else
            {
                return 0;
            }
        }
    }
}