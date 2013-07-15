using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using tasky.DAL;
using tasky.Models;

namespace tasky.Repository
{
    public class TeamMemberRepository : ITeamMemberRepository
    {
        private TaskyContext db = new TaskyContext();

        public IEnumerable<TeamMember> FindAll()
        {
            return db.TeamMembers.ToList();
        }

        public IEnumerable<Task> FindTasksForTeamMember(int id)
        {
            return db.Tasks.Where(model => model.storyId == id).ToList();
        }

        public int Save(TeamMember s)
        {
            if (s.id > 0)
            {
                db.Entry(s).State = EntityState.Modified;
            }
            else
            {
                s = db.TeamMembers.Add(s);
            }
            db.SaveChanges();
            return s.id;
        }

        public TeamMember FindById(int id)
        {
            return db.TeamMembers.Find(id);
        }

        public void Delete(int id)
        {
            TeamMember s = this.FindById(id);
            db.TeamMembers.Remove(s);
            db.SaveChanges();
        }
    }
}