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
        private TaskyContext db = new TaskyContext();

        //
        // GET: /Story/

        public ActionResult Index()
        {
            return View(db.Stories.ToList());
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

        public ActionResult Create()
        {
            ViewBag.StatusOptions = new SelectList(new String[] { "To-Do", "In Progress", "Done", "Accepted"});
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
            ViewBag.StatusOptions = new SelectList(new String[] { "To-Do", "In Progress", "Done", "Accepted" });
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
    }
}