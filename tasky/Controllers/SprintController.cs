using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using tasky.DAL;
using tasky.Models;
using tasky.ViewModels;
using tasky.Repository;

namespace tasky.Controllers
{
    public class SprintController : Controller
    {
        private ISprintRepository repo;
        public SprintController(ISprintRepository r)
        {
            this.repo = r;
        }

        //
        // GET: /Sprint/

        public ActionResult Index()
        {
            return View(repo.FindAll());
        }

        //
        // GET: /Sprint/Details/5

        public ActionResult Details(int id)
        {
            Sprint sprint = repo.FindById(id);

            if (sprint == null)
            {
                return HttpNotFound();
            }

            StoryViewModel storyViewModel = new StoryViewModel();
            SprintViewModel sprintViewModel = new SprintViewModel();
            sprint.stories = repo.FindStoriesForSprint(id);
            sprintViewModel.convertSprint(sprint);

            ICollection<StoryViewModel> stories = new List<StoryViewModel>();
            
            foreach (Story story in sprint.stories)
            {
                stories.Add(StoryViewModel.convertStory(story));
            }

            sprintViewModel.stories = stories;
            
            return View(sprintViewModel);
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
                repo.Save(sprint);
                return RedirectToAction("Index");
            }

            return View(sprint);
        }

        //
        // GET: /Sprint/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Sprint sprint = repo.FindById(id);
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
                repo.Save(sprint);
                return RedirectToAction("Index");
            }
            return View(sprint);
        }


        //
        // GET: /Sprint/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Sprint sprint = repo.FindById(id);

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
            repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}