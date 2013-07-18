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
    public class TaskAPIController : ApiController
    {
        private TaskyContext db = new TaskyContext();

        // GET api/taskapi
        public ICollection<Task> Get()
        {
            return db.Tasks.ToList();
        }

        // GET api/taskapi/5
        public Task Get(int id)
        {
            return db.Tasks.Find(id);
        }

        // POST api/taskapi
        public Task Post([FromBody]Task value)
        {
            if (value != null && ModelState.IsValid)
            {
                value = db.Tasks.Add(value);
                db.SaveChanges();
                return value;
            }
            return null;
        }

        // PUT api/taskapi/5
        public Task Put(int id, [FromBody]Task value)
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

        // DELETE api/taskapi/5
        public void Delete(int id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
            db.SaveChanges();
        }
    }
}
