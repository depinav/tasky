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

namespace UnitTestProject1
{
    [TestClass]
    public class TestSprintController
    {
        [TestMethod]
        public void TestListAll()
        {
            // Arrange
            var mockRepo = new Mock<ISprintRepository>();
            var sprints = new[] { new Sprint { id = 1, title = "test" }, new Sprint { id = 2, title = "test2" } };
            mockRepo.Setup(cr => cr.FindAll()).Returns(sprints);
            var controller = new SprintController(mockRepo.Object);

            // Act
            var result = (ViewResult)controller.Index();

            // Assert
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(IEnumerable<Sprint>));
            var sprintResult = (Sprint[])result.ViewData.Model;
            Assert.AreEqual(2, sprintResult.Length);
        }

        [TestMethod]
        public void TestDetail()
        {
            //verify that the controller returns an error view if the sprint doesn't exist
            var mockRepo = new Mock<ISprintRepository>();
            mockRepo.Setup(cr => cr.FindById(It.IsAny<int>())).Returns((int i) => null);

            var controller = new SprintController(mockRepo.Object);
            var notFoundResult = controller.Details(1);

            Assert.IsInstanceOfType(notFoundResult, typeof(HttpNotFoundResult));


            //verify that the controller returns an a vew for the sprint if it exists
            //also verify that related stories were loaded
            Sprint testSprint = new Sprint { id = 1, title = "abc" };
            Story[] testStories = new Story[] { new Story(), new Story() };
            mockRepo = new Mock<ISprintRepository>();
            mockRepo.Setup(cr => cr.FindById(1)).Returns(testSprint);
            mockRepo.Setup(cr => cr.FindStoriesForSprint(1)).Returns(testStories);

            controller = new SprintController(mockRepo.Object);
            var viewResult = (ViewResult)controller.Details(1);

            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(SprintViewModel));
            var sprintResult = (SprintViewModel)viewResult.ViewData.Model;
            Assert.AreEqual(testSprint.id, sprintResult.id);

            Assert.AreEqual(2, sprintResult.stories.Count);
        }

        [TestMethod]
        public void TestCreate()
        {

            //verify that the argumentless Create() method returns the default create view
            var mockRepo = new Mock<ISprintRepository>();

            var controller = new SprintController(mockRepo.Object);
            var result = (ViewResult)controller.Create();

            Assert.AreEqual("", result.ViewName);


            //verify that when the Create(Sprint) method is passed invalid data
            //the default create view is returned again
            Sprint testSprint = new Sprint { title = "test title" };

            mockRepo = new Mock<ISprintRepository>();
            
            controller = new SprintController(mockRepo.Object);
            controller.ModelState.AddModelError("key", "model is invalid");// Causes ModelState.IsValid to return false
            result = (ViewResult)controller.Create(testSprint);

            Assert.AreEqual("", result.ViewName);
            mockRepo.Verify(cr => cr.Save(It.IsAny<Sprint>()), Times.Never()); //verify that save was never called


            //verify that when the Create(sprint) method is called with a valid sprint,
            //the sprint is inserted and the user is returned to the index page
            testSprint = new Sprint { title = "test title" };
            mockRepo = new Mock<ISprintRepository>();
            mockRepo.Setup(cr => cr.Save(testSprint)).Returns(1);

            controller = new SprintController(mockRepo.Object);
            var routeResult = (RedirectToRouteResult)controller.Create(testSprint);

            mockRepo.Verify(cr => cr.Save(testSprint), Times.Once()); //verify that save was called once
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual(null, routeResult.RouteValues["controller"]);//null because it's the same controller
        }

        [TestMethod]
        public void TestEdit()
        {
            //verify that Edit(id) returns an error view if the sprint doesn't exist
            var mockRepo = new Mock<ISprintRepository>();
            mockRepo.Setup(cr => cr.FindById(It.IsAny<int>())).Returns((int i) => null);

            var controller = new SprintController(mockRepo.Object);
            var notFoundResult = controller.Edit(1);

            Assert.IsInstanceOfType(notFoundResult, typeof(HttpNotFoundResult));



            //verify that the Edit(id) method returns the edit view
            Sprint testSprint = new Sprint { id = 1, title = "abc" };
            mockRepo = new Mock<ISprintRepository>();
            mockRepo.Setup(cr => cr.FindById(1)).Returns(testSprint);

            controller = new SprintController(mockRepo.Object);
            var result = (ViewResult)controller.Edit(1);

            Assert.AreEqual("", result.ViewName);


            //verify that when the Create(Sprint) method is passed invalid data
            //the default create view is returned again
            testSprint = new Sprint { id = 1, title = "test title" };
            mockRepo = new Mock<ISprintRepository>();
            
            controller = new SprintController(mockRepo.Object);
            controller.ModelState.AddModelError("key", "model is invalid");// Causes ModelState.IsValid to return false
            result = (ViewResult)controller.Edit(testSprint);

            Assert.AreEqual("", result.ViewName);
            mockRepo.Verify(cr => cr.Save(It.IsAny<Sprint>()), Times.Never()); //verify that save was never called


            //verify that when the Edit(sprint) method is called with a valid sprint,
            //the sprint is updated and the user is returned to the index page
            testSprint = new Sprint { id = 1, title = "test title" };
            mockRepo = new Mock<ISprintRepository>();
            mockRepo.Setup(cr => cr.Save(testSprint)).Returns(1);

            controller = new SprintController(mockRepo.Object);
            var routeResult = (RedirectToRouteResult)controller.Edit(testSprint);

            //verify that save was called exactly once, with our test sprint as an argument
            mockRepo.Verify(cr => cr.Save(testSprint), Times.Once());
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual(null, routeResult.RouteValues["controller"]);//null because it's the same controller
        }

        [TestMethod]
        public void TestDelete()
        {
            //verify that Delete(id) returns an error view if the sprint doesn't exist
            var mockRepo = new Mock<ISprintRepository>();
            mockRepo.Setup(cr => cr.FindById(It.IsAny<int>())).Returns((int i) => null);

            var controller = new SprintController(mockRepo.Object);
            var notFoundResult = controller.Delete(1);

            Assert.IsInstanceOfType(notFoundResult, typeof(HttpNotFoundResult));



            //verify that the Delete(id) method returns the confirm delete view
            Sprint testSprint = new Sprint { id = 1, title = "abc" };
            mockRepo = new Mock<ISprintRepository>();
            mockRepo.Setup(cr => cr.FindById(1)).Returns(testSprint);

            controller = new SprintController(mockRepo.Object);
            var result = (ViewResult)controller.Delete(1);

            mockRepo.Verify(cr => cr.Delete(testSprint.id), Times.Never());
            Assert.AreEqual("", result.ViewName);


            //verify that the ConfirmDelete(id) method deletes and redirects to the index
            testSprint = new Sprint { id = 1, title = "abc" };
            mockRepo = new Mock<ISprintRepository>();
            mockRepo.Setup(cr => cr.FindById(testSprint.id)).Returns(testSprint);

            controller = new SprintController(mockRepo.Object);
            var routeResult = (RedirectToRouteResult)controller.DeleteConfirmed(testSprint.id);

            mockRepo.Verify(cr => cr.Delete(testSprint.id), Times.Once());
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual(null, routeResult.RouteValues["controller"]);//null because it's the same controller

        }
    }
}
