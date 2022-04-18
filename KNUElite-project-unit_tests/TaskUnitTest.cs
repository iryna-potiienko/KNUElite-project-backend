using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNUElite_project_backend.Controller;
using KNUElite_project_backend.IControllers;
using Moq;
using Xunit;
using KNUElite_project_backend;
using KNUElite_project_backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task = KNUElite_project_backend.Models.Task;
using Type = KNUElite_project_backend.Models.Type;

namespace KNUElite_project_unit_tests
{

    public class TaskUnitTest
    {
        private List<JsonResult> GetTestTasks()
        {
            var users = new List<JsonResult>
            {
                new JsonResult(new {Id = 1, Title = "Tom"}),
                new JsonResult(new {Id = 2, Title = "Alice"}),
                new JsonResult(new {Id = 3, Title = "Sam"}),
                new JsonResult(new {Id = 4, Title = "Kate"})
            };
            return users;
        }

        [Fact]
        public void GetTaskByIdTest()
        {
            //var mock2 = new Mock<IProjectContext>();
            var jsonTask = new JsonResult(new
            {
                Id = 91,
                Title = "TaskTest",
                Description = "Iryna's Test Task",

                TypeId = 31,
                Type = "Task",
                StatusId = 31,
                Status = "In Testing",
                ProjectId = 1,
                Project = "Project",
                EstimatedTime = "",
                LoggedTime = "",

                ReporterId = 1,
                Reporter = "User1",

                AssigneeId = 31,
                Assignee = "Viktoria Kharchenko"
            });
            
            var mock = new Mock<ITaskRepository>();
            mock.Setup(repo => repo.Get(It.IsAny<int>()))
                .Returns(jsonTask);
                // .Returns(new Task()
                // {
                //     Id = 91,
                //     Title = "TaskTest",
                //     Description = "Iryna's Test Task",
                //     LoggedTime = "",
                //     EstimatedTime = "",
                //     AssigneeId = 31,
                //     ReporterId = 1,
                //     StatusId = 31,
                //     ProjectId = 1,
                //     TypeId = 31
                // });

            var controller = new TaskController(mock.Object);

            var result = controller.Get(91);

            var viewResult = Assert.IsType<OkObjectResult>(result.Result);

            var model = Assert.IsType<JsonResult>(viewResult.Value);

            Assert.Equal(jsonTask.Value, model.Value);
            //Assert.Equal("Task1", model.Value.Title);
        }

        [Fact]
        public void GetAllTasksTest()
        {
            // Arrange
            //var mock2 = new Mock<IProjectContext>();
            var mock = new Mock<ITaskRepository>();

            mock.Setup(repo => repo.Get())
                .Returns(GetTestTasks()).Verifiable();


            var controller = new TaskController(mock.Object); //,mock.Object);

            // Act
            var result = controller.Get();

            // Assert

            var viewResult = Assert.IsType<List<JsonResult>>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<JsonResult>>(
                viewResult);
            Assert.Equal(GetTestTasks().Count, model.Count());

            Assert.NotNull(result);
        }

        [Fact]
        public async void AddTaskTest()
        {
            // Arrange
            var task = new Task()
            {
                Title = "TaskTest",
                Description = "Iryna's Test Task",
                LoggedTime = "",
                EstimatedTime = "",
                AssigneeId = 31,
                ReporterId = 1,
                StatusId = 31,
                ProjectId = 1,
                TypeId = 31
            };
            
            //var mockRepo = new Mock<IProjectContext>();
            var mock = new Mock<ITaskRepository>();
            mock.Setup(repo => repo.Save(task))
                .ReturnsAsync(true).Verifiable();

            
            var controller = new TaskController(mock.Object);
            // Act
            var result = await controller.Post(task);

            //Assert
            mock.Verify(r => r.Save(task));

            var redirectToActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Get", redirectToActionResult.ActionName);

            Assert.True(controller.ModelState.IsValid);
            Assert.NotNull(result);
        }

        [Fact]
        public async void DeleteTaskTest()
        {
            //Arrange
            //var mock2 = new Mock<IProjectContext>();
            var mock = new Mock<ITaskRepository>();
            mock.Setup(repo => repo.Delete(1))
                .ReturnsAsync(new Task()
                {
                    Id = 1,
                    Title = "Task1"
                });

            var controller = new TaskController(mock.Object);

            //Act
            var result = await controller.Delete(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
    }
}