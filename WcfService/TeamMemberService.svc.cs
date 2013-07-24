using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TeamMemberService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TeamMemberService.svc or TeamMemberService.svc.cs at the Solution Explorer and start debugging.
    public class TeamMemberService : ITeamMemberService {

        TaskyDBServiceDataContext db = new TaskyDBServiceDataContext();
        
        public ICollection<TeamMember> getTeamMembers() {

            var result = (from teamMember in db.TeamMembers select teamMember);
            return result.ToList();
        }

        public ICollection<TeamMember> getTeamMember(int id) {

            var result = (from teamMember in db.TeamMembers where teamMember.id == id select teamMember);
            return result.ToList();
        }

        public void createTeamMember(string name, string email, string hash, string salt) {

            TeamMember newMember = new TeamMember {

                name = name,
                email = email,
                hash = hash,
                salt = salt
            };

            db.TeamMembers.InsertOnSubmit(newMember);

            try {

                db.SubmitChanges();
            }
            catch (Exception e) {

                Console.WriteLine(e);
            }
        }

        public void deleteTeamMember(int id) {

            var query = (from teamMember in db.TeamMembers where teamMember.id == id select teamMember);
            TeamMember memberToDelete = query.ToList().First();

            db.TeamMembers.DeleteOnSubmit(memberToDelete);

            try {

                db.SubmitChanges();
            }
            catch (Exception e) {

                Console.WriteLine(e);
            }
        }

        public ICollection<TeamMember> updateTeamMember(int id, string name, string email, string hash, string salt) {

            var query = (from teamMember in db.TeamMembers where teamMember.id == id select teamMember);

            foreach (TeamMember member in query) {

                if (name != null)
                    member.name = name;

                if (email != null)
                    member.email = email;

                if (hash != null)
                    member.hash = hash;

                if (salt != null)
                    member.salt = salt;
            }

            try {

                db.SubmitChanges();
            }
            catch (Exception e) {

                Console.WriteLine(e);
            }

            var result = (from teamMember in db.TeamMembers where teamMember.id == id select teamMember);
            return result.ToList();
        }
    }
}
