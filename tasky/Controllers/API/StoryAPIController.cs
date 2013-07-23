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
using tasky.Repository;

namespace tasky.Controllers
{
    public class StoryAPIController : ApiController
    {
        IStoryRepository repo;
        public StoryAPIController(IStoryRepository s)
        {
            repo = s;
        }

        private TaskyContext db = new TaskyContext();

        // GET api/storyapi
        [ActionName("DefaultAction")]
        public ICollection<Story> Get()
        {
            return db.Stories.ToList();
        }

        // GET api/storyapi/5
        [ActionName("DefaultAction")]
        public Story Get(int id)
        {
            return db.Stories.Find(id);
        }

        // POST api/storyapi
        [ActionName("DefaultAction")]
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
        [ActionName("DefaultAction")]
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
        [ActionName("DefaultAction")]
        public void Delete(int id)
        {
            Story story = db.Stories.Find(id);
            db.Stories.Remove(story);
            db.SaveChanges();
        }

        //GET api/storyapi/5/tasks/
        [HttpGet]
        public ICollection<Task> Tasks(int id)
        {
            return db.Tasks.Where(model => model.storyId == id).ToList();
        }

        //POST api/storyapi/saveStories/
        [HttpPost]
        public void saveStories(List<Story> stories)
        {

        }
    }
}
