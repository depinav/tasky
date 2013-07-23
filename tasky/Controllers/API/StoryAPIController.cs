using System.Collections.Generic;
using System.Web.Http;

using tasky.Models;
using tasky.Repository;

namespace tasky.Controllers
{
    public class StoryAPIController : ApiController
    {
        private IStoryRepository repo;
        public StoryAPIController(IStoryRepository s)
        {
            repo = s;
        }

        // GET api/storyapi
        [ActionName("DefaultAction")]
        public ICollection<Story> Get()
        {
            return repo.FindAll();
        }

        // GET api/storyapi/5
        [ActionName("DefaultAction")]
        public Story Get(int id)
        {
            return repo.FindById(id);
        }
        
        // POST api/storyapi
        [ActionName("DefaultAction")]
        public int Post([FromBody]Story value)
        {
            if (value != null && ModelState.IsValid)
            {
                return repo.Save(value);
            }
            return -1;
        }
        
        // PUT api/storyapi/5
        [ActionName("DefaultAction")]
        public void Put(int id, [FromBody]Story value)
        {
            if (value != null && ModelState.IsValid)
            {
                value.id = id;
                repo.Save(value);
            }
        }

        // DELETE api/storyapi/5
        [ActionName("DefaultAction")]
        public void Delete(int id)
        {
            repo.Delete(id);
        }

        //POST api/storyapi/saveStories/
        [HttpPost]
        public void SaveStories([FromBody]List<Story> stories)
        {
            foreach (Story story in stories)
            {
                repo.Save(story);
            }
        }
    }
}
