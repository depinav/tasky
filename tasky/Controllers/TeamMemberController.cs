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
    public class TeamMemberController : Controller
    {
        private TaskyContext db = new TaskyContext();

        //
        // GET: /TeamMember/

        public ActionResult Index()
        {
            return View(db.TeamMembers.ToList());
        }

        //
        // GET: /TeamMember/Details/5

        public ActionResult Details(int id = 0)
        {
            TeamMember teammember = db.TeamMembers.Find(id);
            teammember.tasks = db.Tasks.Where(t => t.TeamMemberId == id).OrderBy(t => t.Title).ToList();
            if (teammember == null)
            {
                return HttpNotFound();
            }
            return View(teammember);
        }

        //
        // GET: /TeamMember/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TeamMember/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TeamMember teammember)
        {
            if (ModelState.IsValid)
            {
                db.TeamMembers.Add(teammember);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(teammember);
        }

        //
        // GET: /TeamMember/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TeamMember teammember = db.TeamMembers.Find(id);
            ViewBag.TaskOptions = new SelectList(getTaskOptions(), "Id", "Title");

            if (teammember == null)
            {
                return HttpNotFound();
            }
            return View(teammember);
        }

        //
        // POST: /TeamMember/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TeamMember teammember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teammember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teammember);
        }

        //
        // GET: /TeamMember/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TeamMember teammember = db.TeamMembers.Find(id);
            if (teammember == null)
            {
                return HttpNotFound();
            }
            return View(teammember);
        }

        //
        // POST: /TeamMember/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeamMember teammember = db.TeamMembers.Find(id);
            db.TeamMembers.Remove(teammember);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private List<Task> getTaskOptions()
        {
            return db.Tasks.ToList();
        }
    }
}