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
using tasky.ViewModels;

namespace tasky.Controllers
{
    public class ReleaseController : Controller
    {
        private IReleaseRepository releaseRepo;
        private ISprintRepository sprintRepo = new SprintRepository();
        private TaskyContext db = new TaskyContext();
        //See below, parameter 'ReleaseRepository'
        public ReleaseController(ReleaseRepository r)
        {
            this.releaseRepo = r;
        }


        //
        // GET: /Default1/

        public ActionResult Index()
        {
            ICollection<Release> releases = releaseRepo.FindAll();
            ICollection<ReleaseViewModel> releaseVM = new List<ReleaseViewModel>();
            IEnumerator<Release> releaseIterator = releases.GetEnumerator();
            ReleaseViewModel tempReleaseVM;
            while (releaseIterator.MoveNext())
            {
                tempReleaseVM = ReleaseViewModel.convertRelease(releaseIterator.Current);
                tempReleaseVM.convertSprintsToVMs(releaseRepo.FindSprintsForRelease(releaseIterator.Current.id));
                releaseVM.Add(tempReleaseVM);
            }
            //return View(db.Releases.ToList());
            return View(releaseVM);
            
        }

        //
        // GET: /Default1/Details/5

        public ActionResult Details(int id = 0)
        {
            Release release = db.Releases.Find(id);
            ReleaseViewModel releaseVM = ReleaseViewModel.convertRelease(release);
            if (release == null)
            {
                return HttpNotFound();
            }
            //return View(release);
            return View(releaseVM);
        }

        //
        // GET: /Default1/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Default1/Create

        [HttpPost]
        public ActionResult Create(Release release)
        {
            if (ModelState.IsValid)
            {
                db.Releases.Add(release);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(release);
        }

        //
        // GET: /Default1/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Release release = db.Releases.Find(id);
            if (release == null)
            {
                return HttpNotFound();
            }
            return View(release);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        public ActionResult Edit(Release release)
        {
            if (ModelState.IsValid)
            {
                db.Entry(release).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(release);
        }

        //
        // GET: /Default1/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Release release = db.Releases.Find(id);
            if (release == null)
            {
                return HttpNotFound();
            }
            return View(release);
        }

        //
        // POST: /Default1/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Release release = db.Releases.Find(id);
            db.Releases.Remove(release);
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