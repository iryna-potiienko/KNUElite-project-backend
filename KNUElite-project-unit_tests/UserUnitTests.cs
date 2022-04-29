using System.Collections.Generic;
using System.Linq;
using KNUElite_project_backend.Controller;
using KNUElite_project_backend.IRepositories;
using KNUElite_project_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;
using Xunit;

namespace KNUElite_project_unit_tests
{
    public class UserUnitTests
    {
        private List<User> GetTestTasks()
        {
            var users = new List<User>
            {
                new User {Id = 1, Name = "TestUser1", RoleId = 32,Role = new Role{Id = 32, Name = "User"}},
                new User {Id = 2, Name = "TestUser2"},
                new User {Id = 3, Name = "TestUser3"}
            };
            return users;
        }

        [Fact]
        public void GetUserByIdTest()
        {

            var user = GetTestTasks().FirstOrDefault(u => u.Id == 1);

            var mock = new Mock<IUserRepository>();
            mock.Setup(repo => repo.Get(It.IsAny<int>()))
                .Returns(user);

            var controller = new UserController(mock.Object);

            var result = controller.Get(91);

            var viewResult = Assert.IsType<OkObjectResult>(result.Result);

            var model = Assert.IsType<User>(viewResult.Value);

            Assert.Equal(user?.Id, model.Id);
            Assert.Equal(user?.Name, model.Name);
        }

        [Fact]
        public void GetAllUsersTest()
        {
            var mock = new Mock<IUserRepository>();

            mock.Setup(repo => repo.Get())
                .Returns(GetTestTasks()).Verifiable();
            
            var controller = new UserController(mock.Object);

            // Act
            var result = controller.Get();

            // Assert
            var viewResult = Assert.IsType<List<User>>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<User>>(
                viewResult);
            Assert.Equal(GetTestTasks().Count, model.Count());

            Assert.NotNull(result);
        }

        [Fact]
        public async void AddUserTest()
        {
            // Arrange
            var testUser = GetTestTasks().FirstOrDefault(user => user.Id == 1);

            var mock = new Mock<IUserRepository>();
            mock.Setup(repo => repo.Add(testUser))
                .ReturnsAsync(true).Verifiable();
            
            var controller = new UserController(mock.Object);
            // Act
            var result = await controller.Post(testUser);

            //Assert
            mock.Verify(r => r.Add(testUser));

            var redirectToActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Get", redirectToActionResult.ActionName);

            Assert.True(controller.ModelState.IsValid);
            Assert.NotNull(result);
        }

        [Fact]
        public async void DeleteUserTest()
        {
            //Arrange
            var testUser = GetTestTasks().FirstOrDefault(user => user.Id == 1);
            
            var mock = new Mock<IUserRepository>();
            mock.Setup(repo => repo.Delete(1))
                .ReturnsAsync(testUser);

            var controller = new UserController(mock.Object);

            //Act
            var result = await controller.Delete(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        
        [Fact]
        public void CheckUser()
        {
            var user = new User
            {
                Id = 1,
                Name = "TestUser",
                Email = "testuser@gmail.com",
                Password = "Password1",
                RoleId = 31,
                Role = new Role{Id = 32, Name = "User"}
            };
            //var user = GetTestTasks().FirstOrDefault(u => u.Id == 1);
            var data = JObject.FromObject(new {email = user?.Email, password = user?.Password});

            var mock = new Mock<IUserRepository>();
            mock.Setup(repo => repo.CheckUser(user.Email, user.Password))
                .Returns(user);

            var controller = new UserController(mock.Object);

            var result = controller.CheckUser(data);

            var viewResult = Assert.IsType<OkObjectResult>(result.Result);

            var model = Assert.IsType<JsonResult>(viewResult.Value);

           // Assert.Equal(user, model.Value);
            //Assert.Equal(user, model.Value);
        }
    }
}