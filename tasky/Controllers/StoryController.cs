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
    public class StoryController : Controller
    {
        private static String[] StatusOptions = new String[] { "To-Do", "In Progress", "Done", "Accepted"};
        private TaskyContext db = new TaskyContext();

        //
        // GET: /Story/

        public ActionResult Index(string statusFilter = "", int? sprintFilter = null)
        {
            //create a selectlist for the status options
            ViewBag.StatusOptions = new SelectList(StatusOptions);

            //create a selectlist for the sprint options - use the name of every existing sprint
            ViewBag.SprintOptions = new SelectList(getSprintOptions(), "Id","Title");

            var storyQuery = db.Stories.AsQueryable();
            if (statusFilter.Length > 0)
            {
                storyQuery = storyQuery.Where(model => model.status == statusFilter);
            }
            if (sprintFilter != null)
            {
                storyQuery = storyQuery.Where(model => model.sprint.id == (int)sprintFilter);
            }
            
            return View(storyQuery.ToList());
        }

        //
        // GET: /Story/Details/5

        public ActionResult Details(int id = 0)
        {
            Story story = db.Stories.Find(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            return View(story);
        }

        //
        // GET: /Story/Create

        public ActionResult Create(int? sprintID)
        {
            ViewBag.StatusOptions = new SelectList(StatusOptions);
            if (sprintID != null)
            {
                Sprint currentSprint = db.Sprints.Find(sprintID);
                ViewBag.currentSprintID = currentSprint.id;
                ViewBag.sprintTitle = currentSprint.title;
            }
            else
            {
                //create a selectlist for the sprint options - use the name of every existing sprint
                ViewBag.SprintOptions = new SelectList(getSprintOptions(), "Id", "Title");
            }

            //create a selectlist for the status options
            ViewBag.StatusOptions = new SelectList(StatusOptions);
            return View();
        }

        //
        // POST: /Story/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Story story)
        {
            if (ModelState.IsValid)
            {
                db.Stories.Add(story);                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(story);
        }

        //
        // GET: /Story/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Story story = db.Stories.Find(id);
            if (story == null)
            {
                return HttpNotFound();
            }

            //create a selectlist for the status options
            ViewBag.StatusOptions = new SelectList(StatusOptions);

            //create a selectlist for the sprint options - use the name of every existing sprint
            ViewBag.SprintOptions = new SelectList(getSprintOptions(), "Id", "Title");

            return View(story);
        }

        //
        // POST: /Story/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Story story)
        {
            if (ModelState.IsValid)
            {
                db.Entry(story).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //create a selectlist for the status options
            ViewBag.StatusOptions = new SelectList(StatusOptions);

            //create a selectlist for the sprint options - use the name of every existing sprint
            ViewBag.SprintOptions = new SelectList(getSprintOptions(), "Id", "Title");

            return View(story);
        }

        //
        // GET: /Story/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Story story = db.Stories.Find(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            return View(story);
        }

        //
        // POST: /Story/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Story story = db.Stories.Find(id);
            db.Stories.Remove(story);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //queries all stories, groups them by sprint name, then returns the sprint names
        private List<Sprint> getSprintOptions()
        {
            return db.Sprints.ToList();
        }
    }
}