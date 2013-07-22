using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using tasky.DAL;
using tasky.Models;
using tasky.ViewModels;
using tasky.Repository;
using System;

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
            ICollection<Sprint> sprints = repo.FindAll();
            ICollection<SprintViewModel> sprintVMs = new List<SprintViewModel>();
            IEnumerator<Sprint> sprintIterator = sprints.GetEnumerator();
            SprintViewModel tempSprintVM;
            while (sprintIterator.MoveNext())
            {
                tempSprintVM = SprintViewModel.convertSprint(sprintIterator.Current);
                tempSprintVM.convertStoriesToVMs(repo.FindStoriesForSprint(sprintIterator.Current.id));
                sprintVMs.Add(tempSprintVM);
            }
            
            return View(sprintVMs);
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
            sprint.stories = repo.FindStoriesForSprint(id);
            SprintViewModel sprintViewModel = SprintViewModel.convertSprint(sprint);

            ICollection<StoryViewModel> stories = new List<StoryViewModel>();
            
            foreach (Story story in sprint.stories)
            {
                stories.Add(StoryViewModel.convertStory(story));
            }

            sprintViewModel.stories = stories;

            //compute the data for the burndown chart
            int totalEstimated = repo.SumTaskEstimatesForSprint(id);

            Dictionary<DateTime, int> dataMap = new Dictionary<DateTime, int>();
            foreach (var i in repo.FindTaskLogsForSprint(id))
            {
                int sum;
                if (!dataMap.TryGetValue(i.logDate, out sum))
                {
                    sum = 0;
                }
                dataMap[i.logDate] = sum + i.loggedHours;
            }

            //create the list of "actual" burndown data
            List<BurndownEntry> burndownEntries = new List<BurndownEntry>();
            int remainingHours = totalEstimated;
            foreach(DateTime d in dataMap.Keys.OrderBy(date => date)) {
                remainingHours -= dataMap[d];
                burndownEntries.Add(new BurndownEntry { date = d, remaining = remainingHours });
            }

            //create the list of "ideal" burndown data
            List<BurndownEntry> idealBurn = new List<BurndownEntry>();
            idealBurn.Add(new BurndownEntry { date = sprint.startDate, remaining = totalEstimated });
            idealBurn.Add(new BurndownEntry { date = sprint.endDate, remaining = 0 });

            ViewBag.ActualBurndownEntries = burndownEntries;
            ViewBag.IdealBurndownEntries = idealBurn;
            ViewBag.InitialEstimate = totalEstimated;
            
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

        [HttpPost]
        public void UpdateStories(int id, List<Story> stories)
        {
            repo.SaveStories(id, stories);
        }

        public class BurndownEntry
        {
            public DateTime date { get; set; }
            public int remaining { get; set; }
        }

    }
}