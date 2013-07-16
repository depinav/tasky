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
    }
}
