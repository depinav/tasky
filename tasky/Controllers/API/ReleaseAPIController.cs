using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using System.Web;
using tasky.Repository;
using tasky.Models;

namespace tasky.Controllers.API
{
    public class ReleaseAPIController : ApiController
    {
        private IReleaseRepository repo;
        public ReleaseAPIController(ReleaseRepository r)
        {
            repo = r;
        }

        public ICollection<Release> Get()
        {
            return repo.FindAll();
        }

        public Release Get(int id)
        {
            return repo.FindById(id);
        }

        public int Post([FromBody]Release val)
        {
            if (val != null && ModelState.IsValid)
            {
                return repo.Save(val);
            }
            return -1;
        }

        public void Put(int id, [FromBody]Release val)
        {
            if (val != null && ModelState.IsValid)
            {
                val.id = id;
                repo.Save(val);
            }
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

    }
}