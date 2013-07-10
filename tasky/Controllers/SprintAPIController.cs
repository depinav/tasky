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
        public IEnumerable<Sprint> Get()
        {
            return db.Sprints.ToList();
        }

        // GET api/sprintapi/5
        public Sprint Get(int id)
        {
            return db.Sprints.Find(id);
        }

        // POST api/sprintapi
        public Sprint Post([FromBody]SprintAPI value)
        {
            if (value != null && ModelState.IsValid)
            {
                Sprint result = new Sprint();
                result.startDate = (DateTime)value.startDate;
                result.endDate = (DateTime)value.endDate;
                result.title = value.title;

                result = db.Sprints.Add(result);
                db.SaveChanges();
                return result;
            }
            return null;
        }

        // PUT api/sprintapi/5
        public Sprint Put(int id, [FromBody]Sprint value)
        {
            value.id = id;
            db.Entry(value).State = EntityState.Modified;
            db.SaveChanges();
            return value;
        }

        // DELETE api/sprintapi/5
        public void Delete(int id)
        {
            Sprint sprint = db.Sprints.Find(id);
            db.Sprints.Remove(sprint);
            db.SaveChanges();
        }
    }
}
