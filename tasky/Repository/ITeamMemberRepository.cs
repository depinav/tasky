using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tasky.Models;

namespace tasky.Repository
{
    public interface ITeamMemberRepository
    {
        IEnumerable<TeamMember> FindAll();
        IEnumerable<Task> FindTasksForTeamMember(int id);
        int Save(TeamMember s);
        TeamMember FindById(int id);
        void Delete(int id);
    }
}
