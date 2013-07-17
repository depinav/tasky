using System;
using System.Web;
using System.Web.Mvc;
using tasky.Models;
using tasky.DAL;
using tasky.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tasky.Controllers;
using System.Collections.Generic;
using Moq;
using tasky.ViewModels;

namespace Tests
{
    [TestClass]
    public class TestTaskController
    {
        [TestMethod]
        public void TestTaskIndex()
        {
            //verify that when index is called without filters, all stories are returned
            var taskRepo = new Mock<ITaskRepository>();
            var storyRepo = getStoryMock();
            var teamMemberRepo = getTeamMemberMock();

            var stories = new List<Task> { new Task { id = 1, Title = "test" }, new Task { id = 2, Title = "test2" } };
            taskRepo.Setup(cr => cr.FindWithFilters("", null)).Returns(stories);
            var controller = new TaskController(storyRepo.Object, teamMemberRepo.Object, taskRepo.Object);

            var result = (ViewResult)controller.Index();

            Assert.IsInstanceOfType(result.ViewData.Model, typeof(ICollection<Task>));
            var taskResult = (ICollection<Task>)result.ViewData.Model;
            Assert.AreEqual(2, taskResult.Count);

            //verify that when called with a status filter and story filter
            //the controller passes the filters to the repo
            taskRepo = new Mock<ITaskRepository>();
            storyRepo = getStoryMock();
            teamMemberRepo = getTeamMemberMock();

            stories = new List<Task> { new Task { id = 1, Title = "test" }, new Task { id = 2, Title = "test2" } };
            taskRepo.Setup(cr => cr.FindWithFilters("abc", 1)).Returns(stories);
            controller = new TaskController(storyRepo.Object, teamMemberRepo.Object, taskRepo.Object);

            result = (ViewResult)controller.Index("abc", 1);

            Assert.IsInstanceOfType(result.ViewData.Model, typeof(ICollection<Task>));
            taskResult = (ICollection<Task>)result.ViewData.Model;
            Assert.AreEqual(2, taskResult.Count);
        }

        [TestMethod]
        public void TestTaskDetail()
        {
            //verify that the controller returns an error view if the story doesn't exist
            var taskRepo = new Mock<ITaskRepository>();
            taskRepo.Setup(cr => cr.FindById(It.IsAny<int>())).Returns((int i) => null);

            var controller = new TaskController(null, null, taskRepo.Object);
            var notFoundResult = controller.Details(1);

            Assert.IsInstanceOfType(notFoundResult, typeof(HttpNotFoundResult));


            //verify that the controller returns an a vew for the story if it exists
            //also verify that related stories were loaded
            Task testTask = new Task { id = 1, Title = "abc" };
            Task[] testTasks = new Task[] { new Task(), new Task() };
            taskRepo = new Mock<ITaskRepository>();
            taskRepo.Setup(cr => cr.FindById(1)).Returns(testTask);

            controller = new TaskController(null, null, taskRepo.Object);
            var viewResult = (ViewResult)controller.Details(1);

            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(Task));
            var taskResult = (Task)viewResult.ViewData.Model;
            Assert.AreEqual(testTask.id, taskResult.id);
        }

        [TestMethod]
        public void TestTaskCreate()
        {

            //verify that the argumentless Create() method returns the default create view
            var taskRepo = new Mock<ITaskRepository>();
            var storyRepo = getStoryMock();
            var teamMemberRepo = getTeamMemberMock();

            var controller = new TaskController(storyRepo.Object, teamMemberRepo.Object, taskRepo.Object);
            var result = (ViewResult)controller.Create();

            Assert.AreEqual("", result.ViewName);


            //verify that when the Create(Task) method is passed invalid data
            //the default create view is returned again
            Task testTask = new Task { Title = "test Title" };

            taskRepo = new Mock<ITaskRepository>();

            controller = new TaskController(null, null, taskRepo.Object);
            controller.ModelState.AddModelError("key", "model is invalid");// Causes ModelState.IsValid to return false
            result = (ViewResult)controller.Create(testTask);

            Assert.AreEqual("", result.ViewName);
            taskRepo.Verify(cr => cr.Save(It.IsAny<Task>()), Times.Never()); //verify that save was never called


            //verify that when the Create(story) method is called with a valid story,
            //the story is inserted and the user is returned to the index page
            testTask = new Task { Title = "test Title" };
            taskRepo = new Mock<ITaskRepository>();
            taskRepo.Setup(cr => cr.Save(testTask)).Returns(1);

            controller = new TaskController(null, null, taskRepo.Object);
            var routeResult = (RedirectToRouteResult)controller.Create(testTask);

            taskRepo.Verify(cr => cr.Save(testTask), Times.Once()); //verify that save was called once
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual(null, routeResult.RouteValues["controller"]);//null because it's the same controller
        }

        [TestMethod]
        public void TestTaskEdit()
        {
            //verify that Edit(id) returns an error view if the story doesn't exist
            var taskRepo = new Mock<ITaskRepository>();
            taskRepo.Setup(cr => cr.FindById(It.IsAny<int>())).Returns((int i) => null);

            var controller = new TaskController(null, null, taskRepo.Object);
            var notFoundResult = controller.Edit(1);

            Assert.IsInstanceOfType(notFoundResult, typeof(HttpNotFoundResult));



            //verify that the Edit(id) method returns the edit view
            Task testTask = new Task { id = 1, Title = "abc" };
            taskRepo = new Mock<ITaskRepository>();
            var storyRepo = getStoryMock();
            var teamMemberRepo = getTeamMemberMock();
            taskRepo.Setup(cr => cr.FindById(1)).Returns(testTask);

            controller = new TaskController(storyRepo.Object, teamMemberRepo.Object, taskRepo.Object);
            var result = (ViewResult)controller.Edit(1);

            Assert.AreEqual("", result.ViewName);


            //verify that when the Create(Task) method is passed invalid data
            //the default create view is returned again
            testTask = new Task { id = 1, Title = "test Title" };
            taskRepo = new Mock<ITaskRepository>();
            storyRepo = getStoryMock();

            controller = new TaskController(storyRepo.Object, null, taskRepo.Object);
            controller.ModelState.AddModelError("key", "model is invalid");// Causes ModelState.IsValid to return false
            result = (ViewResult)controller.Edit(testTask);

            Assert.AreEqual("", result.ViewName);
            taskRepo.Verify(cr => cr.Save(It.IsAny<Task>()), Times.Never()); //verify that save was never called


            //verify that when the Edit(story) method is called with a valid story,
            //the story is updated and the user is returned to the index page
            testTask = new Task { id = 1, Title = "test Title" };
            taskRepo = new Mock<ITaskRepository>();
            taskRepo.Setup(cr => cr.Save(testTask)).Returns(1);

            controller = new TaskController(null, null, taskRepo.Object);
            var routeResult = (RedirectToRouteResult)controller.Edit(testTask);

            //verify that save was called exactly once, with our test story as an argument
            taskRepo.Verify(cr => cr.Save(testTask), Times.Once());
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual(null, routeResult.RouteValues["controller"]);//null because it's the same controller
        }

        [TestMethod]
        public void TestTaskDelete()
        {
            //verify that Delete(id) returns an error view if the story doesn't exist
            var taskRepo = new Mock<ITaskRepository>();
            taskRepo.Setup(cr => cr.FindById(It.IsAny<int>())).Returns((int i) => null);

            var controller = new TaskController(null, null, taskRepo.Object);
            var notFoundResult = controller.Delete(1);

            Assert.IsInstanceOfType(notFoundResult, typeof(HttpNotFoundResult));



            //verify that the Delete(id) method returns the confirm delete view
            Task testTask = new Task { id = 1, Title = "abc" };
            taskRepo = new Mock<ITaskRepository>();
            taskRepo.Setup(cr => cr.FindById(1)).Returns(testTask);

            controller = new TaskController(null, null, taskRepo.Object);
            var result = (ViewResult)controller.Delete(1);

            taskRepo.Verify(cr => cr.Delete(testTask.id), Times.Never());
            Assert.AreEqual("", result.ViewName);


            //verify that the ConfirmDelete(id) method deletes and redirects to the index
            testTask = new Task { id = 1, Title = "abc" };
            taskRepo = new Mock<ITaskRepository>();
            taskRepo.Setup(cr => cr.FindById(testTask.id)).Returns(testTask);

            controller = new TaskController(null, null, taskRepo.Object);
            var routeResult = (RedirectToRouteResult)controller.DeleteConfirmed(testTask.id);

            taskRepo.Verify(cr => cr.Delete(testTask.id), Times.Once());
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual(null, routeResult.RouteValues["controller"]);//null because it's the same controller

        }

        private Mock<IStoryRepository> getStoryMock()
        {
            Mock<IStoryRepository> m = new Mock<IStoryRepository>();

            var storys = new[] { new Story { id = 1, title = "test" }, new Story { id = 2, title = "test2" } };
            m.Setup(cr => cr.FindAll()).Returns(storys);

            return m;
        }

        private Mock<ITeamMemberRepository> getTeamMemberMock()
        {
            Mock<ITeamMemberRepository> m = new Mock<ITeamMemberRepository>();

            var storys = new[] { new TeamMember { id = 1, name = "test" }, new TeamMember { id = 2, name = "test2" } };
            m.Setup(cr => cr.FindAll()).Returns(storys);

            return m;
        }

        private List<String> expectedStatusOptions()
        {
            return new List<String> { "To-Do", "In Progress", "Done", "Accepted" };
        }
    }
}
