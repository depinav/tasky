using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tasky.Models;
using tasky.ViewModels;
using tasky.DAL;
using tasky.Repository;

namespace tasky.Controllers
{
    public class StoryController : Controller
    {
        private static String[] StatusOptions = new String[] { "To-Do", "In Progress", "Done", "Accepted"};

        private ISprintRepository sprintRepo;
        private IStoryRepository storyRepo;
        public StoryController(ISprintRepository s,IStoryRepository r)
        {
            this.sprintRepo = s;
            this.storyRepo = r;
        }

        //
        // GET: /Story/

        public ActionResult Index(string statusFilter = "", int? sprintFilter = null)
        {
            //create a selectlist for the status options
            ViewBag.StatusOptions = new SelectList(StatusOptions);

            //create a selectlist for the sprint options - use the name of every existing sprint
            ViewBag.SprintOptions = new SelectList(getSprintOptions(), "Id","Title");

            return View(storyRepo.FindWithFilters(statusFilter, sprintFilter));
        }

        //
        // GET: /Story/Details/5


        public ActionResult Details(int id)
        {
            SprintViewModel sprintViewModel = new SprintViewModel();

            Story story = storyRepo.FindById(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            story.tasks = storyRepo.FindTasksForStory(id);

            StoryViewModel storyViewModel = StoryViewModel.convertStory(story);
            sprintViewModel.convertSprint(story.sprint);
            storyViewModel.sprintViewModel = sprintViewModel;
            ICollection<TaskViewModel> tasks = new List<TaskViewModel>();

            foreach(Task task in story.tasks)
            {
                tasks.Add(TaskViewModel.convertTask(task));
            }

            storyViewModel.tasks = tasks;
            return View(storyViewModel);
        }

        //
        // GET: /Story/Create

        public ActionResult Create(int? sprintID = null)
        {
            ViewBag.StatusOptions = new SelectList(StatusOptions);
            if (sprintID != null)
            {
                Sprint currentSprint = sprintRepo.FindById((int)sprintID);
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
                storyRepo.Save(story);
                return RedirectToAction("Index");
            }
            return View(story);
        }

        //
        // GET: /Story/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Story story = storyRepo.FindById(id);
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
                storyRepo.Save(story);
                return RedirectToAction("Index");
            }

            return View(story);
        }

        //
        // GET: /Story/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Story story = storyRepo.FindById(id);
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
            storyRepo.Delete(id);
            return RedirectToAction("Index");
        }

        //queries all stories, groups them by sprint name, then returns the sprint names
        private IEnumerable<Sprint> getSprintOptions()
        {
            return sprintRepo.FindAll();
        }
    }
}