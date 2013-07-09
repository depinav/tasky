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
    public class SprintController : Controller
    {
        private TaskyContext db = new TaskyContext();

        //
        // GET: /Sprint/

        public ActionResult Index()
        {
            return View(db.Sprints.ToList());
        }

        //
        // GET: /Sprint/Details/5

        public ActionResult Details(int id)
        {
            Sprint sprint = db.Sprints.Find(id);
            if (sprint == null)
            {
                return HttpNotFound();
            }
            return View(sprint);
        }

        //
        // GET: /Sprint/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Sprint/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Sprint sprint)
        {
            if (ModelState.IsValid)
            {
                db.Sprints.Add(sprint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sprint);
        }

        //
        // GET: /Sprint/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Sprint sprint = db.Sprints.Find(id);
            if (sprint == null)
            {
                return HttpNotFound();
            }
            return View(sprint);
        }

        //
        // POST: /Sprint/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Sprint sprint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sprint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sprint);
        }


        //
        // GET: /Sprint/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Sprint sprint = db.Sprints.Find(id);
            if (sprint == null)
            {
                return HttpNotFound();
            }
            return View(sprint);
        }

        //
        // POST: /Sprint/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sprint sprint = db.Sprints.Find(id);
            db.Sprints.Remove(sprint);
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