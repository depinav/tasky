using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService {

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SprintService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SprintService.svc or SprintService.svc.cs at the Solution Explorer and start debugging.
    public class SprintService : ISprintService {

        TaskyDBServiceDataContext db = new TaskyDBServiceDataContext();

        public ICollection<Sprint> getAllSprints()
        {

            var result = (from sprint in db.Sprints select sprint);
            return result.ToList();
        }

        public ICollection<Sprint> getSprint(int id) {

            var result = (from sprint in db.Sprints where sprint.id == id select sprint);
            return result.ToList();
        }

        public void createSprint(string newTitle, DateTime newStartDate, DateTime newEndDate) {

            Sprint newSprint = new Sprint {

                title = newTitle,
                startDate = newStartDate,
                endDate = newEndDate
            };

            db.Sprints.InsertOnSubmit(newSprint);

            try {

                db.SubmitChanges();
            }
            catch(Exception e) {

                Console.WriteLine(e);
            }
        }

        public void deleteSprint(int id) {

            var result = (from sprint in db.Sprints where sprint.id == id select sprint);
            Sprint sprintToDelete = result.ToList().First();

            db.Sprints.DeleteOnSubmit(sprintToDelete);

            try {

                db.SubmitChanges();
            }
            catch (Exception e) {

                Console.WriteLine(e);
            }
        }

        public ICollection<Sprint> updateSprint(int id, string newTitle, DateTime newStartDate, DateTime newEndDate) {

            var query = (from sprint in db.Sprints where sprint.id == id select sprint);
            
            foreach(Sprint sprintToUpdate in query) {

                sprintToUpdate.title = newTitle;
                
                sprintToUpdate.startDate = newStartDate;
                sprintToUpdate.endDate = newEndDate;
           }

            try {

                db.SubmitChanges();
            }
            catch (Exception e) {

                Console.WriteLine(e);
            }

            var result = (from updatedSprint in db.Sprints where updatedSprint.id == id select updatedSprint);
            return result.ToList();
        }
    }
}
