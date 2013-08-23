using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tasky.Repository;
using tasky.Models;
using tasky.ViewModels;
using tasky.Controllers;
using Moq;

namespace UnitTestProject1.Tests
{
    [TestClass]
    class TestReleaseController
    {
        [TestMethod]
        public void TestReleaseIndex()
        {
            var mockRepo = new Mock<ReleaseRepository>();
            var releases = new List<Release> { new Release { id = 1, title = "test" }, new Release { id = 2, title = "test2" } };
            mockRepo.Setup(cr => cr.FindAll()).Returns(releases);
            var controller = new ReleaseController(mockRepo.Object);

            var result = (ViewResult)controller.Index();

            Assert.IsInstanceOfType(result.ViewData.Model, typeof(ICollection<ReleaseViewModel>));
            var releaseResult = (List<ReleaseViewModel>)result.ViewData.Model;
            Assert.AreEqual(2, releaseResult.Count);
        }

        [TestMethod]
        public void TestReleaseDetail()
        {
            var mockRepo = new Mock<ReleaseRepository>();
            mockRepo.Setup(cr => cr.FindById(It.IsAny<int>())).Returns((int i) => null);

            var controller = new ReleaseController(mockRepo.Object);
            var notFoundResult = controller.Details(1);

            Assert.IsInstanceOfType(notFoundResult, typeof(HttpNotFoundResult));

            Release testRelease = new Release{
                id=1,
                title="abc"
            };
            Sprint[] testSprints = new Sprint[] {new Sprint(), new Sprint()};

            mockRepo = new Mock<ReleaseRepository>();
            mockRepo.Setup(cr => cr.FindById(1)).Returns(testRelease);
            mockRepo.Setup(cr => cr.FindSprintsForRelease(1)).Returns(testSprints);

            controller = new ReleaseController(mockRepo.Object);
            var viewResult = (ViewResult)controller.Details(1);

            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(ReleaseViewModel));
            var releaseResult = (ReleaseViewModel)viewResult.ViewData.Model;
            Assert.AreEqual(testRelease.id, releaseResult.id);

            Assert.AreEqual(2, releaseResult.sprints.Count); 
        }

        [TestMethod]
        public void TestReleaseCreate()
        {
            var mockRepo = new Mock<ReleaseRepository>();

            var controller = new ReleaseController(mockRepo.Object);
            var result = (ViewResult)controller.Create();

            Assert.AreEqual("", result.ViewName);


            Release testRelease = new Release { title="test title" };

            mockRepo = new Mock<ReleaseRepository>();

            controller = new ReleaseController(mockRepo.Object);
            controller.ModelState.AddModelError("key", "model is invalid");
            result = (ViewResult)controller.Create(testRelease);

            Assert.AreEqual("", result.ViewName);
            mockRepo.Verify(cr => cr.Save(It.IsAny<Release>()), Times.Never());

            testRelease = new Release { title = "test title"};
            mockRepo = new Mock<ReleaseRepository>();
            mockRepo.Setup(cr => cr.Save(testRelease)).Returns(1);

            controller = new ReleaseController(mockRepo.Object);
            var routeResult = (RedirectToRouteResult)controller.Create(testRelease);

            mockRepo.Verify(cr => cr.Save(testRelease), Times.Once());
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual(null, routeResult.RouteValues["controller"]);

        }

        [TestMethod]
        public void TestReleaseEdit()
        {
            var mockRepo = new Mock<ReleaseRepository>();
            mockRepo.Setup(cr => cr.FindById(It.IsAny<int>())).Returns((int i) => null);

            var controller = new ReleaseController(mockRepo.Object);
            var notFoundResult = controller.Edit(1);

            Assert.IsInstanceOfType(notFoundResult, typeof(HttpNotFoundResult));

            Release testRelease = new Release { id = 1, title = "asdf" };
            mockRepo = new Mock<ReleaseRepository>();
            mockRepo.Setup(cr => cr.FindById(1)).Returns(testRelease);

            controller = new ReleaseController(mockRepo.Object);
            var result = (ViewResult)controller.Edit(1);

            Assert.AreEqual("", result.ViewName);

            testRelease = new Release { id = 1, title = "test title" };
            mockRepo = new Mock<ReleaseRepository>();

            controller = new ReleaseController(mockRepo.Object);
            controller.ModelState.AddModelError("key", "model is invalid");
            result = (ViewResult)controller.Edit(testRelease);

            Assert.AreEqual("", result.ViewName);
            mockRepo.Verify(cr => cr.Save(It.IsAny<Release>()), Times.Never());

            testRelease = new Release { id = 1, title = "test title" };
            mockRepo = new Mock<ReleaseRepository>();
            mockRepo.Setup(cr => cr.Save(testRelease)).Returns(1);

            controller = new ReleaseController(mockRepo.Object);
            var routeResult = (RedirectToRouteResult)controller.Edit(testRelease);

            mockRepo.Verify(cr => cr.Save(testRelease), Times.Once());
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual(null, routeResult.RouteValues["controller"]);


        }

        [TestMethod]
        public void TestReleaseDelete()
        {
            var mockRepo = new Mock<ReleaseRepository>();
            mockRepo.Setup(cr => cr.FindById(It.IsAny<int>())).Returns((int i) => null);

            var controller = new ReleaseController(mockRepo.Object);
            var notFoundResult = controller.Delete(1);

            Assert.IsInstanceOfType(notFoundResult, typeof(HttpNotFoundResult));


            Release testRelease = new Release { id = 1, title = "asdf" };
            mockRepo = new Mock<ReleaseRepository>();
            mockRepo.Setup(cr => cr.FindById(1)).Returns(testRelease);

            controller = new ReleaseController(mockRepo.Object);
            var result = (ViewResult)controller.Delete(1);

            mockRepo.Verify(cr => cr.Delete(testRelease.id), Times.Never());
            Assert.AreEqual("", result.ViewName);

            testRelease = new Release { id = 1, title = "test title" };
            mockRepo = new Mock<ReleaseRepository>();
            mockRepo.Setup(cr => cr.FindById(testRelease.id)).Returns(testRelease);

            controller = new ReleaseController(mockRepo.Object);
            var routeResult = (RedirectToRouteResult)controller.DeleteConfirmed(testRelease.id);

            mockRepo.Verify(cr => cr.Delete(testRelease.id), Times.Once());
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual(null, routeResult.RouteValues["controller"]);


        }
    }
}
