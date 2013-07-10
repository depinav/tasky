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
    public class StoryAPIController : ApiController
    {
        private TaskyContext db = new TaskyContext();

        // GET api/storyapi
        public IEnumerable<Story> Get()
        {
            return db.Stories.ToList();
        }

        // GET api/storyapi/5
        public Story Get(int id)
        {
            return db.Stories.Find(id);
        }

        // POST api/storyapi
        public Story Post([FromBody]Story value)
        {
            if (value != null && ModelState.IsValid)
            {
                value = db.Stories.Add(value);
                db.SaveChanges();
                return value;
            }
            return null;
        }

        // PUT api/storyapi/5
        public Story Put(int id, [FromBody]Story value)
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

        // DELETE api/storyapi/5
        public void Delete(int id)
        {
            Story story = db.Stories.Find(id);
            db.Stories.Remove(story);
            db.SaveChanges();
        }
    }
}
