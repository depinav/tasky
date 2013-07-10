using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tasky.Models;
using tasky.DAL;

namespace tasky.Controllers
{
    public class TaskController : Controller
    {
        private static String[] StatusOptions = new String[] { "To-Do", "In Progress", "Done", "Accepted" };
        private TaskyContext db = new TaskyContext();

        //
        // GET: /Task/

        public ActionResult Index(string statusFilter = "", int? teamMemberFilter = null)
        {
            //create a selectlist for the status options
            ViewBag.StatusOptions = new SelectList(StatusOptions);

            //create a selectlist for the team member options - use the name of every existing sprint
            ViewBag.TeamMemberOptions = new SelectList(getTeamMemberOptions(), "Id", "Name");

            var taskQuery = db.Tasks.AsQueryable();
            if (statusFilter.Length > 0)
            {
                taskQuery = taskQuery.Where(model => model.Status == statusFilter);
            }
            if (teamMemberFilter != null)
            {
                taskQuery = taskQuery.Where(model => model.TeamMember.id == (int)teamMemberFilter);
            }

            return View(taskQuery.ToList());
            //return View(db.Tasks.ToList());
        }

        //
        // GET: /Task/Details/5

        public ActionResult Details(int id = 0)
        {
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        //
        // GET: /Task/Create

        public ActionResult Create(int? teamMemberID, int? storyID)
        {
            ViewBag.StatusOptions = new SelectList(StatusOptions);
            if (teamMemberID != null)
            {
                TeamMember currentTeamMember = db.TeamMembers.Find(teamMemberID);
                ViewBag.currentTeamMemberID = currentTeamMember.id;
                ViewBag.teamMemberName = currentTeamMember.name;
            }
            else
            {
                //create a selectlist for the team member options - use the name of every existing member
                ViewBag.TeamMemberOptions = new SelectList(getTeamMemberOptions(), "Id", "Name");
            }

            if (storyID != null)
            {
                Story currentStory = db.Stories.Find(storyID);
                ViewBag.currentStoryID = currentStory.id;
                ViewBag.currentStoryTitle = currentStory.title;
            }
            else
            {
                ViewBag.StoryOptions = new SelectList(getStoryOptions(), "Id", "Title");
            }

            //create a selectlist for the status options
            ViewBag.StatusOptions = new SelectList(StatusOptions);
            return View();

            //List<TeamMember> TeamMemberList = db.TeamMembers.OrderBy(model => model.name).ToList();
            //ViewBag.TeamList = new SelectList(TeamMemberList, "id", "Name");
            //return View();
        }

        //
        // POST: /Task/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Task task)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(task);
        }

        //
        // GET: /Task/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }

            //create a selectlist for the status options
            ViewBag.StatusOptions = new SelectList(StatusOptions);

            //create a selectlist for the team member options - use the name of every existing member
            ViewBag.TeamMemberOptions = new SelectList(getTeamMemberOptions(), "Id", "Name");

            ViewBag.StoryOptions = new SelectList(getStoryOptions(), "Id", "Title");

            return View(task);
        }

        //
        // POST: /Task/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(task);
        }

        //
        // GET: /Task/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        //
        // POST: /Task/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //queries all tasks, groups them by team member name, then returns the names
        private List<TeamMember> getTeamMemberOptions()
        {
            return db.TeamMembers.ToList();
        }

        private List<Story> getStoryOptions()
        {
            return db.Stories.ToList();
        }
    }
}