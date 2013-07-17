using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;
using tasky.Controllers;
using tasky.Models;
using tasky.Repository;
using tasky.ViewModels;

namespace Tests
{
    [TestClass]
    public class TestTeamMemberController
    {
        [TestMethod]
        public void TestTeamMemberIndex()
        {
            // Arrange
            var mockRepo = new Mock<ITeamMemberRepository>();
            var members = new[] { new TeamMember { id = 1, name = "test" }, new TeamMember { id = 2, name = "test2" } };
            mockRepo.Setup(cr => cr.FindAll()).Returns(members);
            var controller = new TeamMemberController(mockRepo.Object);

            // Act
            var result = (ViewResult)controller.Index();

            // Assert
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(IEnumerable<TeamMember>));
            var memberResult = (TeamMember[])result.ViewData.Model;
            Assert.AreEqual(2, memberResult.Length);
        }

        [TestMethod]
        public void TestTeamMemberDetail()
        {
            //verify that the controller returns an error view if the member doesn't exist
            var mockRepo = new Mock<ITeamMemberRepository>();
            mockRepo.Setup(cr => cr.FindById(It.IsAny<int>())).Returns((int i) => null);

            var controller = new TeamMemberController(mockRepo.Object);
            var notFoundResult = controller.Details(1);

            Assert.IsInstanceOfType(notFoundResult, typeof(HttpNotFoundResult));


            //verify that the controller returns an a view for the member if it exists
            //also verify that related tasks were loaded
            TeamMember testMember = new TeamMember { id = 1, name = "testy" };
            Task[] testTasks = new Task[] { new Task(), new Task() };
            mockRepo = new Mock<ITeamMemberRepository>();
            mockRepo.Setup(cr => cr.FindById(1)).Returns(testMember);
            mockRepo.Setup(cr => cr.FindTasksForTeamMember(1)).Returns(testTasks);

            controller = new TeamMemberController(mockRepo.Object);
            var viewResult = (ViewResult)controller.Details(1);

            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(TeamMember));
            var memberResult = (TeamMember)viewResult.ViewData.Model;
            Assert.AreEqual(testMember.id, memberResult.id);

            // fix this
            //Assert.AreEqual(2, memberResult.tasks);
        }

        [TestMethod]
        public void TestTeamMemberCreate()
        {

            //verify that the argumentless Create() method returns the default create view
            var mockRepo = new Mock<ITeamMemberRepository>();

            var controller = new TeamMemberController(mockRepo.Object);
            var result = (ViewResult)controller.Create();

            Assert.AreEqual("", result.ViewName);


            //verify that when the Create(TeamMember) method is passed invalid data
            //the default create view is returned again
            TeamMember testMember = new TeamMember { name = "test name" };

            mockRepo = new Mock<ITeamMemberRepository>();

            controller = new TeamMemberController(mockRepo.Object);
            controller.ModelState.AddModelError("key", "model is invalid");// Causes ModelState.IsValid to return false
            result = (ViewResult)controller.Create(testMember);

            Assert.AreEqual("", result.ViewName);
            mockRepo.Verify(cr => cr.Save(It.IsAny<TeamMember>()), Times.Never()); //verify that save was never called


            //verify that when the Create(team member) method is called with a valid team member,
            //the team member is inserted and the user is returned to the team member page
            testMember = new TeamMember { name = "test name" };
            mockRepo = new Mock<ITeamMemberRepository>();
            mockRepo.Setup(cr => cr.Save(testMember)).Returns(1);

            controller = new TeamMemberController(mockRepo.Object);
            var routeResult = (RedirectToRouteResult)controller.Create(testMember);

            mockRepo.Verify(cr => cr.Save(testMember), Times.Once()); //verify that save was called once
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual(null, routeResult.RouteValues["controller"]);//null because it's the same controller
        }

        [TestMethod]
        public void TestTeamMemberEdit()
        {
            //verify that Edit(id) returns an error view if the member doesn't exist
            var mockRepo = new Mock<ITeamMemberRepository>();
            mockRepo.Setup(cr => cr.FindById(It.IsAny<int>())).Returns((int i) => null);

            var controller = new TeamMemberController(mockRepo.Object);
            var notFoundResult = controller.Edit(1);

            Assert.IsInstanceOfType(notFoundResult, typeof(HttpNotFoundResult));



            //verify that the Edit(id) method returns the edit view
            TeamMember testMember = new TeamMember { id = 1, name = "test" };
            mockRepo = new Mock<ITeamMemberRepository>();
            mockRepo.Setup(cr => cr.FindById(1)).Returns(testMember);

            controller = new TeamMemberController(mockRepo.Object);
            var result = (ViewResult)controller.Edit(1);

            Assert.AreEqual("", result.ViewName);


            //verify that when the Create(TeamMember) method is passed invalid data
            //the default create view is returned again
            testMember = new TeamMember { id = 1, name = "testy" };
            mockRepo = new Mock<ITeamMemberRepository>();

            controller = new TeamMemberController(mockRepo.Object);
            controller.ModelState.AddModelError("key", "model is invalid");// Causes ModelState.IsValid to return false
            result = (ViewResult)controller.Edit(testMember);

            Assert.AreEqual("", result.ViewName);
            mockRepo.Verify(cr => cr.Save(It.IsAny<TeamMember>()), Times.Never()); //verify that save was never called


            //verify that when the Edit(TeamMember) method is called with a valid member,
            //the member is updated and the user is returned to the index page
            testMember = new TeamMember { id = 1, name = "testy" };
            mockRepo = new Mock<ITeamMemberRepository>();
            mockRepo.Setup(cr => cr.Save(testMember)).Returns(1);

            controller = new TeamMemberController(mockRepo.Object);
            var routeResult = (RedirectToRouteResult)controller.Edit(testMember);

            //verify that save was called exactly once, with our test member as an argument
            mockRepo.Verify(cr => cr.Save(testMember), Times.Once());
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual(null, routeResult.RouteValues["controller"]);//null because it's the same controller
        }

        [TestMethod]
        public void TestTeamMemberDelete()
        {
            //verify that Delete(id) returns an error view if the member doesn't exist
            var mockRepo = new Mock<ITeamMemberRepository>();
            mockRepo.Setup(cr => cr.FindById(It.IsAny<int>())).Returns((int i) => null);

            var controller = new TeamMemberController(mockRepo.Object);
            var notFoundResult = controller.Delete(1);

            Assert.IsInstanceOfType(notFoundResult, typeof(HttpNotFoundResult));



            //verify that the Delete(id) method returns the confirm delete view
            TeamMember testMember = new TeamMember { id = 1, name = "test" };
            mockRepo = new Mock<ITeamMemberRepository>();
            mockRepo.Setup(cr => cr.FindById(1)).Returns(testMember);

            controller = new TeamMemberController(mockRepo.Object);
            var result = (ViewResult)controller.Delete(1);

            mockRepo.Verify(cr => cr.Delete(testMember.id), Times.Never());
            Assert.AreEqual("", result.ViewName);


            //verify that the ConfirmDelete(id) method deletes and redirects to the index
            testMember = new TeamMember { id = 1, name = "test" };
            mockRepo = new Mock<ITeamMemberRepository>();
            mockRepo.Setup(cr => cr.FindById(testMember.id)).Returns(testMember);

            controller = new TeamMemberController(mockRepo.Object);
            var routeResult = (RedirectToRouteResult)controller.DeleteConfirmed(testMember.id);

            mockRepo.Verify(cr => cr.Delete(testMember.id), Times.Once());
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual(null, routeResult.RouteValues["controller"]);//null because it's the same controller
        }

    }
}
