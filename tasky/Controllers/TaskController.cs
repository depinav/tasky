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
    public class TaskController : Controller
    {
        private static String[] StatusOptions = new String[] { "To-Do", "In Progress", "Done", "Accepted" };

        private IStoryRepository storyRepo;
        private ITeamMemberRepository teamMemberRepo;
        private ITaskRepository taskRepo;

        public TaskController(IStoryRepository s, ITeamMemberRepository m, ITaskRepository r)
        {
            this.storyRepo = s;
            this.teamMemberRepo = m;
            this.taskRepo = r;
        }

        //
        // GET: /Task/

        public ActionResult Index(string statusFilter = "", int? teamMemberFilter = null)
        {
            //create a selectlist for the status options
            ViewBag.StatusOptions = new SelectList(StatusOptions);

            //create a selectlist for the team member options - use the name of every existing sprint
            ViewBag.TeamMemberOptions = new SelectList(getTeamMemberOptions(), "Id", "Name");

            return View(taskRepo.FindWithFilters(statusFilter, teamMemberFilter));
            //return View(db.Tasks.ToList());
        }

        //
        // GET: /Task/Details/5

        public ActionResult Details(int id = 0)
        {
            Task task = taskRepo.FindById(id);
            TaskLogViewModel taskLogVM = new TaskLogViewModel();
            taskLogVM.getTaskInfo(task);
            taskLogVM.getTaskLogInfo( new TaskLog { taskId = task.id });

            if (task == null)
            {
                return HttpNotFound();
            }

            return View(taskLogVM);
        }

        //
        // GET: /Task/Create

        public ActionResult Create(int? teamMemberID = null, int? storyID = null)
        {
            ViewBag.StatusOptions = new SelectList(StatusOptions);
            
            if (teamMemberID != null)
            {
                TeamMember currentTeamMember = teamMemberRepo.FindById((int)teamMemberID);
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
                Story currentStory = storyRepo.FindById((int)storyID);
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
                taskRepo.Save(task);
                return RedirectToAction("Index");
            }

            return View(task);
        }

        public ActionResult LogHours(TaskLog log)
        {
            if (ModelState.IsValid)
            {
                Task task = taskRepo.FindById(log.taskId);
                task.Remaining_Hours -= log.loggedHours;

                taskRepo.Log(log);
                taskRepo.Save(task);
            }
            return RedirectToAction("Details/"+log.taskId);;
        }

        //
        // GET: /Task/Edit/5

        public ActionResult Edit(int id)
        {
            Task task = taskRepo.FindById(id);
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
                taskRepo.Save(task);
                return RedirectToAction("Index");
            }

            return View(task);
        }

        //
        // GET: /Task/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Task task = taskRepo.FindById(id);
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
            taskRepo.Delete(id);
            return RedirectToAction("Index");
        }

        //queries all tasks, groups them by team member name, then returns the names
        private ICollection<TeamMember> getTeamMemberOptions()
        {
            return teamMemberRepo.FindAll();
        }

        private ICollection<Story> getStoryOptions()
        {
            return storyRepo.FindAll();
        }
    }
}