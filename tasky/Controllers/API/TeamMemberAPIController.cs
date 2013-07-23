using System.Collections.Generic;
using System.Web.Http;

using tasky.Models;
using tasky.Repository;

namespace tasky.Controllers
{
    public class TeamMemberAPIController : ApiController
    {
        private ITeamMemberRepository repo;
        public TeamMemberAPIController(ITeamMemberRepository s)
        {
            repo = s;
        }

        // GET api/teammemberapi
        public ICollection<TeamMember> Get()
        {
            return repo.FindAll();
        }

        // GET api/teammemberapi/5
        public TeamMember Get(int id)
        {
            return repo.FindById(id);
        }

        // POST api/teammemberapi
        public int Post([FromBody]TeamMember value)
        {
            if (value != null && ModelState.IsValid)
            {
                return repo.Save(value);
            }
            return -1;
        }

        // PUT api/teammemberapi/5
        public void Put(int id, [FromBody]TeamMember value)
        {
            if (value != null && ModelState.IsValid)
            {
                repo.Save(value);
            }
        }

        // DELETE api/teammemberapi/5
        public void Delete(int id)
        {
            repo.Delete(id);
        }
    }
}
