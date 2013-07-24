using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITeamMemberService" in both code and config file together.
    [ServiceContract]
    public interface ITeamMemberService
    {
        [OperationContract]
        ICollection<TeamMember> getTeamMembers();

        [OperationContract]
        ICollection<TeamMember> getTeamMember(int id);

        [OperationContract]
        void createTeamMember(string name, string email, string hash, string salt);

        [OperationContract]
        void deleteTeamMember(int id);

        [OperationContract]
        ICollection<TeamMember> updateTeamMember(int id, string name, string email, string hash, string salt);
    }
}
