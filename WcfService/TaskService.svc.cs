using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using tasky.DAL;
using tasky.Models;
using tasky.Repository;

namespace WcfService
{
    public class TaskService : ITaskService {

        TaskyDBServiceDataContext db = new TaskyDBServiceDataContext();

        public ICollection<Task> getTasks() {

            //return taskRepo.FindAll();
            var result = (from task in db.Tasks select task);
            return result.ToList();
        }

        public ICollection<Task> getTask(int id) {

            var result = (from task in db.Tasks where task.id == id select task);

            if (result != null)
                return result.ToList();
            else
                return null;
        }

        public ICollection<Task> createTask(string title, string description, int hours, int teamMemberID, int storyID) {

            var theTeamMember = (from teamMember in db.TeamMembers where teamMember.id == teamMemberID select teamMember);
            int myTeamMember = theTeamMember.ToList().First().id;
            var theStory = (from story in db.Stories where story.id == storyID select story);
            int myStory = theStory.ToList().First().id;

            Task task = new Task {
                Title = title,
                Description = description,
                Estimate_Hours = hours,
                Remaining_Hours = hours,
                Status = "To do",
                TeamMemberId = myTeamMember,
                storyId = myStory
            };

            db.Tasks.InsertOnSubmit(task);

            try {

                db.SubmitChanges();
            }
            catch(Exception e) {

                Console.WriteLine(e);
            }

            var result = (from aTask in db.Tasks select aTask);
            return result.ToList();
        }

        public ICollection<Task> deleteTask(int id) {

            var taskQueried = (from task in db.Tasks where task.id == id select task);
            Task taskToDelete = taskQueried.ToList().First();

            db.Tasks.DeleteOnSubmit(taskToDelete);

            try {

                db.SubmitChanges();
            }
            catch (Exception e) {

                Console.WriteLine(e);
            }

            var result = (from aTask in db.Tasks select aTask);
            return result.ToList();
        }

        public ICollection<Task> updateTask(int id, string title, string description, string estimateHours, string remainingHours, string teamMemberID, string storyID) {

            var query = (from atask in db.Tasks where atask.id == id select atask);

            foreach (Task task in query) {

                if (title != null)
                    task.Title = title;

                if (description != null)
                    task.Description = description;

                if (estimateHours != null)
                    task.Estimate_Hours = int.Parse(estimateHours);

                if (remainingHours != null)
                    task.Remaining_Hours = int.Parse(remainingHours);

                if (teamMemberID != null)
                {

                    TeamMember teamMember = (from team in db.TeamMembers where team.id == int.Parse(teamMemberID) select team).ToList().First();
                    task.TeamMemberId = teamMember.id;
                }

                if (storyID != null)
                {

                    Story story = (from aStory in db.Stories where aStory.id == int.Parse(storyID) select aStory).ToList().First();
                    task.storyId = story.id;
                }
            }

            try {

                db.SubmitChanges();
            }
            catch (Exception e) {

                Console.WriteLine(e);
            }

            var result = (from aTask in db.Tasks select aTask);
            return result.ToList();
        }
    }
}
