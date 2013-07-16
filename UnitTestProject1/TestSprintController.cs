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

            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(Sprint));
            var sprintResult = (Sprint)viewResult.ViewData.Model;
            Assert.AreEqual(testSprint, sprintResult);

            Assert.IsInstanceOfType(sprintResult.stories, typeof(Story[]));
            Assert.AreEqual(2, ((Story[])sprintResult.stories).Length);
        }

        [TestMethod]
        public void TestCreate()
        {

            //verify that the argumentless Create() method returns the default create view
            var mockRepo = new Mock<ISprintRepository>();

            var controller = new SprintController(mockRepo.Object);
            var result = (ViewResult)controller.Create();

            Assert.AreEqual("", result.ViewName);

            
            //verify that when the Create(Sprint) method is passed an invalid sprint
            //the default create view is returned
            Sprint testSprint = new Sprint { title = "test title" };

            mockRepo = new Mock<ISprintRepository>();
            mockRepo.Setup(cr => cr.Save(It.IsAny<Sprint>())).Throws(new InvalidOperationException("Save should not have been called, since the model is invalid"));
            
            controller = new SprintController(mockRepo.Object);
            controller.ModelState.AddModelError("key", "model is invalid");// Causes ModelState.IsValid to return false
            result = (ViewResult)controller.Create(testSprint);

            Assert.AreEqual("", result.ViewName);


            //verify that when the Create(sprint) method is called with a valid sprint,
            //the sprint is inserted and the user is returned to the index page
            testSprint = new Sprint { title = "test title" };
            mockRepo = new Mock<ISprintRepository>();
            mockRepo.Setup(cr => cr.Save(testSprint)).Returns(1);

            controller = new SprintController(mockRepo.Object);
            var routeResult = (RedirectToRouteResult)controller.Create(testSprint);

            //verify that save was called exactly once, with our test sprint as an argument
            mockRepo.Verify(cr => cr.Save(testSprint), Times.Once());
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual(null, routeResult.RouteValues["controller"]);//null because it's the same controller
        }
    }
}
