using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using tasky.DAL;
using tasky.Models;

namespace tasky.Repository
{
    public class ReleaseRepository : IReleaseRepository
    {
        TaskyContext db = new TaskyContext();

        public ICollection<Release> FindAll(){
            return db.Releases.ToList();
        }

        public ICollection<Sprint> FindSprintsForRelease(int id)
        {

            return db.Sprints
                    .Where(s => s.releaseId == id)
                    .OrderBy(s => s.title).ToList();
        }

        public int Save(Release r)
        {
            if (r.id > 0)
            {
                db.Entry(r).State = EntityState.Modified;
            }
            else
            {
                r = db.Releases.Add(r);
            }
            db.SaveChanges();
            return r.id;
        }

        public Release FindById(int id)
        {
            Release res = db.Releases.Find(id);
            if(res != null){
                res.sprints = db.Sprints
                    .Where(s => s.releaseId == id)
                    .OrderBy(s => s.title).ToList();
            }
            return res;
        }

        public void Delete(int id)
        {
            Release release = this.FindById(id);
            db.Releases.Remove(release);
            db.SaveChanges();
        }
    }
}