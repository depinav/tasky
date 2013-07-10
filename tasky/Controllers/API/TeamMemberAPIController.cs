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
    public class TeamMemberAPIController : ApiController
    {
        private TaskyContext db = new TaskyContext();

        // GET api/teammemberapi
        public IEnumerable<TeamMember> Get()
        {
            return db.TeamMembers.ToList();
        }

        // GET api/teammemberapi/5
        public TeamMember Get(int id)
        {
            return db.TeamMembers.Find(id);
        }

        // POST api/teammemberapi
        public TeamMember Post([FromBody]TeamMember value)
        {
            if (value != null && ModelState.IsValid)
            {
                value = db.TeamMembers.Add(value);
                db.SaveChanges();
                return value;
            }
            return null;
        }

        // PUT api/teammemberapi/5
        public TeamMember Put(int id, [FromBody]TeamMember value)
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

        // DELETE api/teammemberapi/5
        public void Delete(int id)
        {
            TeamMember teammember = db.TeamMembers.Find(id);
            db.TeamMembers.Remove(teammember);
            db.SaveChanges();
        }
    }
}
