using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using tasky.DAL;
using tasky.Models;

namespace tasky.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private TaskyContext db = new TaskyContext();

        public ICollection<Task> FindAll()
        {
            return db.Tasks.ToList();
        }

        public ICollection<Task> FindWithFilters(string statusFilter, int? teamMemberFilter)
        {
            var taskQuery = db.Tasks.AsQueryable();
            if (statusFilter.Length > 0)
            {
                taskQuery = taskQuery.Where(model => model.Status == statusFilter);
            }
            if (teamMemberFilter != null)
            {
                taskQuery = taskQuery.Where(model => model.TeamMember.id == (int)teamMemberFilter);
            }

            return taskQuery.ToList();
        }

        public int Save(Task s)
        {
            if (s.id > 0)
            {
                db.Entry(s).State = EntityState.Modified;
            }
            else
            {
                s = db.Tasks.Add(s);
            }
            db.SaveChanges();
            return s.id;
        }

        public void Log(TaskLog log)
        {
            //save taskLog data here. one could even say you need to... 'log' the taskLog
            log = db.TaskLogs.Add(log);
        }

        public Task FindById(int id)
        {
            return db.Tasks.Find(id);
        }

        public void Delete(int id)
        {
            Task s = this.FindById(id);
            db.Tasks.Remove(s);
            db.SaveChanges();
        }
    }
}