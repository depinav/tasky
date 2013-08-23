using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tasky.Models;

namespace tasky.Repository
{
    public interface IReleaseRepository
    {
        ICollection<Release> FindAll();
        ICollection<Sprint> FindSprintsForRelease(int id);
        int Save(Release r);
        Release FindById(int id);
        void Delete(int id);

        //What other data access do we need here?
    }
}