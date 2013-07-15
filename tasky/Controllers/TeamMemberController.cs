using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tasky.Models;
using tasky.DAL;
using tasky.Repository;

namespace tasky.Controllers
{
    public class TeamMemberController : Controller
    {
        private ITeamMemberRepository repo;
        public TeamMemberController(ITeamMemberRepository r)
        {
            this.repo = r;
        }

        //
        // GET: /TeamMember/

        public ActionResult Index()
        {
            return View(repo.FindAll());
        }

        //
        // GET: /TeamMember/Details/5

        public ActionResult Details(int id = 0)
        {
            TeamMember teammember = repo.FindById(id);

            if (teammember == null)
            {
                return HttpNotFound();
            }
            else
            {
                teammember.tasks = repo.FindTasksForTeamMember(id);
                return View(teammember);
            }
            
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
                repo.Save(teammember);
                return RedirectToAction("Index");
            }

            return View(teammember);
        }

        //
        // GET: /TeamMember/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TeamMember teammember = repo.FindById(id);

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
                repo.Save(teammember);
                return RedirectToAction("Index");
            }
            return View(teammember);
        }

        //
        // GET: /TeamMember/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TeamMember teammember = repo.FindById(id);
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
            repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}