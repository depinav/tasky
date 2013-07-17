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
    public class TestStoryController
    {
        [TestMethod]
        public void TestStoryIndex()
        {
            //verify that when index is called without filters, all stories are returned
            var storyRepo = new Mock<IStoryRepository>();
            var sprintRepo = getSprintMock();

            var stories = new List<Story> { new Story { id = 1, title = "test" }, new Story { id = 2, title = "test2" } };
            storyRepo.Setup(cr => cr.FindWithFilters("", null)).Returns(stories);
            var controller = new StoryController(sprintRepo.Object, storyRepo.Object);

            var result = (ViewResult)controller.Index();

            Assert.IsInstanceOfType(result.ViewData.Model, typeof(ICollection<Story>));
            var storyResult = (ICollection<Story>)result.ViewData.Model;
            Assert.AreEqual(2, storyResult.Count);

            //verify that when called with a status filter and sprint filter
            //the controller passes the filters to the repo
            storyRepo = new Mock<IStoryRepository>();
            sprintRepo = getSprintMock();

            stories = new List<Story> { new Story { id = 1, title = "test" }, new Story { id = 2, title = "test2" } };
            storyRepo.Setup(cr => cr.FindWithFilters("abc", 1)).Returns(stories);
            controller = new StoryController(sprintRepo.Object, storyRepo.Object);

            result = (ViewResult)controller.Index("abc", 1);

            Assert.IsInstanceOfType(result.ViewData.Model, typeof(ICollection<Story>));
            storyResult = (ICollection<Story>)result.ViewData.Model;
            Assert.AreEqual(2, storyResult.Count);
        }

        [TestMethod]
        public void TestStoryDetail()
        {
            //verify that the controller returns an error view if the sprint doesn't exist
            var storyRepo = new Mock<IStoryRepository>();
            storyRepo.Setup(cr => cr.FindById(It.IsAny<int>())).Returns((int i) => null);

            var controller = new StoryController(null, storyRepo.Object);
            var notFoundResult = controller.Details(1);

            Assert.IsInstanceOfType(notFoundResult, typeof(HttpNotFoundResult));


            //verify that the controller returns an a vew for the sprint if it exists
            //also verify that related stories were loaded
            Story testStory = new Story { id = 1, title = "abc" };
            Task[] testTasks = new Task[] { new Task(), new Task() };
            storyRepo = new Mock<IStoryRepository>();
            storyRepo.Setup(cr => cr.FindById(1)).Returns(testStory);
            storyRepo.Setup(cr => cr.FindTasksForStory(1)).Returns(testTasks);

            controller = new StoryController(null, storyRepo.Object);
            var viewResult = (ViewResult)controller.Details(1);

            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(StoryViewModel));
            var sprintResult = (StoryViewModel)viewResult.ViewData.Model;
            Assert.AreEqual(testStory.id, sprintResult.id);

            Assert.AreEqual(2, sprintResult.tasks.Count);
        }

        [TestMethod]
        public void TestStoryCreate()
        {

            //verify that the argumentless Create() method returns the default create view
            var storyRepo = new Mock<IStoryRepository>();
            var sprintRepo = getSprintMock();

            var controller = new StoryController(sprintRepo.Object, storyRepo.Object);
            var result = (ViewResult)controller.Create();

            Assert.AreEqual("", result.ViewName);


            //verify that when the Create(Story) method is passed invalid data
            //the default create view is returned again
            Story testStory = new Story { title = "test title" };

            storyRepo = new Mock<IStoryRepository>();
            
            controller = new StoryController(null, storyRepo.Object);
            controller.ModelState.AddModelError("key", "model is invalid");// Causes ModelState.IsValid to return false
            result = (ViewResult)controller.Create(testStory);

            Assert.AreEqual("", result.ViewName);
            storyRepo.Verify(cr => cr.Save(It.IsAny<Story>()), Times.Never()); //verify that save was never called


            //verify that when the Create(sprint) method is called with a valid sprint,
            //the sprint is inserted and the user is returned to the index page
            testStory = new Story { title = "test title" };
            storyRepo = new Mock<IStoryRepository>();
            storyRepo.Setup(cr => cr.Save(testStory)).Returns(1);

            controller = new StoryController(null, storyRepo.Object);
            var routeResult = (RedirectToRouteResult)controller.Create(testStory);

            storyRepo.Verify(cr => cr.Save(testStory), Times.Once()); //verify that save was called once
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual(null, routeResult.RouteValues["controller"]);//null because it's the same controller
        }

        [TestMethod]
        public void TestStoryEdit()
        {
            //verify that Edit(id) returns an error view if the sprint doesn't exist
            var storyRepo = new Mock<IStoryRepository>();
            storyRepo.Setup(cr => cr.FindById(It.IsAny<int>())).Returns((int i) => null);

            var controller = new StoryController(null, storyRepo.Object);
            var notFoundResult = controller.Edit(1);

            Assert.IsInstanceOfType(notFoundResult, typeof(HttpNotFoundResult));



            //verify that the Edit(id) method returns the edit view
            Story testStory = new Story { id = 1, title = "abc" };
            storyRepo = new Mock<IStoryRepository>();
            var sprintRepo = getSprintMock();
            storyRepo.Setup(cr => cr.FindById(1)).Returns(testStory);

            controller = new StoryController(sprintRepo.Object, storyRepo.Object);
            var result = (ViewResult)controller.Edit(1);

            Assert.AreEqual("", result.ViewName);


            //verify that when the Create(Story) method is passed invalid data
            //the default create view is returned again
            testStory = new Story { id = 1, title = "test title" };
            storyRepo = new Mock<IStoryRepository>();
            sprintRepo = getSprintMock();

            controller = new StoryController(sprintRepo.Object, storyRepo.Object);
            controller.ModelState.AddModelError("key", "model is invalid");// Causes ModelState.IsValid to return false
            result = (ViewResult)controller.Edit(testStory);

            Assert.AreEqual("", result.ViewName);
            storyRepo.Verify(cr => cr.Save(It.IsAny<Story>()), Times.Never()); //verify that save was never called


            //verify that when the Edit(sprint) method is called with a valid sprint,
            //the sprint is updated and the user is returned to the index page
            testStory = new Story { id = 1, title = "test title" };
            storyRepo = new Mock<IStoryRepository>();
            storyRepo.Setup(cr => cr.Save(testStory)).Returns(1);

            controller = new StoryController(null, storyRepo.Object);
            var routeResult = (RedirectToRouteResult)controller.Edit(testStory);

            //verify that save was called exactly once, with our test sprint as an argument
            storyRepo.Verify(cr => cr.Save(testStory), Times.Once());
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual(null, routeResult.RouteValues["controller"]);//null because it's the same controller
        }

        [TestMethod]
        public void TestStoryDelete()
        {
            //verify that Delete(id) returns an error view if the sprint doesn't exist
            var storyRepo = new Mock<IStoryRepository>();
            storyRepo.Setup(cr => cr.FindById(It.IsAny<int>())).Returns((int i) => null);

            var controller = new StoryController(null, storyRepo.Object);
            var notFoundResult = controller.Delete(1);

            Assert.IsInstanceOfType(notFoundResult, typeof(HttpNotFoundResult));



            //verify that the Delete(id) method returns the confirm delete view
            Story testStory = new Story { id = 1, title = "abc" };
            storyRepo = new Mock<IStoryRepository>();
            storyRepo.Setup(cr => cr.FindById(1)).Returns(testStory);

            controller = new StoryController(null, storyRepo.Object);
            var result = (ViewResult)controller.Delete(1);

            storyRepo.Verify(cr => cr.Delete(testStory.id), Times.Never());
            Assert.AreEqual("", result.ViewName);


            //verify that the ConfirmDelete(id) method deletes and redirects to the index
            testStory = new Story { id = 1, title = "abc" };
            storyRepo = new Mock<IStoryRepository>();
            storyRepo.Setup(cr => cr.FindById(testStory.id)).Returns(testStory);

            controller = new StoryController(null, storyRepo.Object);
            var routeResult = (RedirectToRouteResult)controller.DeleteConfirmed(testStory.id);

            storyRepo.Verify(cr => cr.Delete(testStory.id), Times.Once());
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual(null, routeResult.RouteValues["controller"]);//null because it's the same controller

        }

        private Mock<ISprintRepository> getSprintMock()
        {
            Mock<ISprintRepository> m = new Mock<ISprintRepository>();

            var sprints = new[] { new Sprint { id = 1, title = "test" }, new Sprint { id = 2, title = "test2" } };
            m.Setup(cr => cr.FindAll()).Returns(sprints);

            return m;
        }

        private List<String> expectedStatusOptions()
        {
            return new List<String> { "To-Do", "In Progress", "Done", "Accepted" };
        }
    }
}
