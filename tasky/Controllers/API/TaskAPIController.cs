using System.Collections.Generic;
using System.Web.Http;

using tasky.Models;
using tasky.Repository;

namespace tasky.Controllers
{
    public class TaskAPIController : ApiController
    {
        private ITaskRepository repo;
        public TaskAPIController(ITaskRepository s)
        {
            repo = s;
        }

        // GET api/taskapi
        public ICollection<Task> Get()
        {
            return repo.FindAll();
        }

        // GET api/taskapi/5
        public Task Get(int id)
        {
            return repo.FindById(id);
        }

        // POST api/taskapi
        public int Post([FromBody]Task value)
        {
            if (value != null && ModelState.IsValid)
            {
                return repo.Save(value);
            }
            return -1;
        }

        // PUT api/taskapi/5
        public void Put(int id, [FromBody]Task value)
        {
            if (value != null && ModelState.IsValid)
            {
                repo.Save(value);
            }
        }

        // DELETE api/taskapi/5
        public void Delete(int id)
        {
            repo.Delete(id);
        }
    }
}
