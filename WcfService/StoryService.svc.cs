using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService {
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "StoryService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select StoryService.svc or StoryService.svc.cs at the Solution Explorer and start debugging.
    public class StoryService : IStoryService {

        TaskyDBServiceDataContext db = new TaskyDBServiceDataContext();
        
        public ICollection<Story> getStories() {

            var result = (from story in db.Stories select story);
            return result.ToList();
        }

        public ICollection<Story> getStory(int id) {

            var result = (from story in db.Stories where story.id == id select story);
            return result.ToList();
        }

        public void createStory(string title, string description, int points, int sprintID) {

            var query = (from sprint in db.Sprints where sprint.id == sprintID select sprint);
            Sprint getSprintID = query.ToList().First();

            Story newStory = new Story {

                title = title,
                description = description,
                points = points,
                status = "To-Do",
                sprintId = getSprintID.id
            };

            db.Stories.InsertOnSubmit(newStory);

            try {

                db.SubmitChanges();
            }
            catch (Exception e) {

                Console.WriteLine(e);
            }
        }

        public void deleteStory(int id) { 

            var query = (from story in db.Stories where story.id == id select story);
            Story storyToDelete = query.ToList().First();

            db.Stories.DeleteOnSubmit(storyToDelete);

            try {

                db.SubmitChanges();
            }
            catch (Exception e) {

                Console.WriteLine(e);
            }
        }

        public ICollection<Story> updateStory(int id, string title, string description, int points, string status, int sprintID) {

            var query = (from story in db.Stories where story.id == id select story);
            Sprint sprintQuery = (from sprint in db.Sprints where sprint.id == sprintID select sprint).ToList().First();

            foreach (Story aStory in query) {

                if(title != null)
                    aStory.title = title;

                if (description != null)
                    aStory.description = description;

                aStory.points = points;

                if (status != null)
                    aStory.status = status;

                aStory.sprintId = sprintQuery.id;
            }

            try {

                db.SubmitChanges();
            }
            catch (Exception e) {

                Console.WriteLine(e);
            }

            var result = (from newStory in db.Stories where newStory.id == id select newStory);
            return result.ToList();
        }
    }
}
