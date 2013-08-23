using System.Collections.Generic;
using System.Web.Http;

using tasky.Models;
using tasky.Repository;

namespace tasky.Controllers
{
    public class SprintAPIController : ApiController
    {

        private ISprintRepository repo;
        public SprintAPIController(ISprintRepository s)
        {
            repo = s;
        }

        // GET api/sprintapi
        public ICollection<Sprint> Get()
        {
            return repo.FindAll();
        }

        // GET api/sprintapi/5
        public Sprint Get(int id)
        {
            return repo.FindById(id);
        }

        // POST api/sprintapi
        public int Post([FromBody]Sprint value)
        {
            if (value != null && ModelState.IsValid)
            {
                return repo.Save(value);
            }
            return -1;
        }

        // PUT api/sprintapi/5
        public void Put(int id, [FromBody]Sprint value)
        {
            if (value != null && ModelState.IsValid)
            {
                value.id = id;
                repo.Save(value);
            }
        }

        // DELETE api/sprintapi/5
        public void Delete(int id)
        {
            repo.Delete(id);
        }

        [HttpPost]
        public void SaveSprints([FromBody]List<Sprint> sprints)
        {
            foreach(Sprint sprint in sprints){
                repo.Save(sprint);
            }
        }
    }
}
