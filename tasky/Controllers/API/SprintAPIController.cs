using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using tasky.Models;
using tasky.DAL;

namespace tasky.Controllers
{
    public class SprintAPIController : ApiController
    {
        private TaskyContext db = new TaskyContext();

        // GET api/sprintapi
        [ActionName("DefaultAction")]
        public ICollection<Sprint> Get()
        {
            return db.Sprints.ToList();
        }

        // GET api/sprintapi/5
        [ActionName("DefaultAction")]
        public Sprint Get(int id)
        {
            return db.Sprints.Find(id);
        }

        // POST api/sprintapi
        [ActionName("DefaultAction")]
        public Sprint Post([FromBody]Sprint value)
        {
            if (value != null && ModelState.IsValid)
            {
                value = db.Sprints.Add(value);
                db.SaveChanges();
                return value;
            }
            return null;
        }

        // PUT api/sprintapi/5
        [ActionName("DefaultAction")]
        public Sprint Put(int id, [FromBody]Sprint value)
        {
            if (value != null && ModelState.IsValid)
            {
                value.id = id;
                db.Entry(value).State = EntityState.Modified;
                db.SaveChanges();
                return value;
            }
            return null;
        }

        // DELETE api/sprintapi/5
        [ActionName("DefaultAction")]
        public void Delete(int id)
        {
            Sprint sprint = db.Sprints.Find(id);
            db.Sprints.Remove(sprint);
            db.SaveChanges();
        }
    }
}
