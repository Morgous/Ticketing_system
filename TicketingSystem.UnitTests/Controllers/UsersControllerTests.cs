using Microsoft.AspNetCore.Mvc;
using Moq;
using TicketingSystem.API.Controllers;
using TicketingSystem.Application.Services;
using TicketingSystem.Domain.Models;
using TicketingSystem.Repositories.Interfaces;
using Xunit;

namespace TicketingSystem.UnitTests.Controllers
{
    public class UsersControllerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UserService _userService;
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();

            _userService = new UserService(_userRepositoryMock.Object);

            _controller = new UsersController(_userService);
        }

        [Fact]
        public async Task GetAllUsers_ShouldReturnOkResult_WithListOfUsers()
        {
            var users = new List<User> { new User { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" } };
            _userRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);

            var result = await _controller.GetAllUsers() as OkObjectResult;

            Assert.NotNull(result);
            var returnValue = Assert.IsType<List<User>>(result.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnOkResult_WhenUserExists()
        {
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);

            var result = await _controller.GetUserById(userId) as OkObjectResult;

            Assert.NotNull(result);
            var returnValue = Assert.IsType<User>(result.Value);
            Assert.Equal(userId, returnValue.Id);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            var userId = Guid.NewGuid();
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync((User)null);

            var result = await _controller.GetUserById(userId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateUser_ShouldReturnCreatedAtActionResult()
        {
            var newUser = new User { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            _userRepositoryMock.Setup(repo => repo.AddAsync(newUser)).Returns(Task.CompletedTask);

            var result = await _controller.CreateUser(newUser) as CreatedAtActionResult;

            Assert.NotNull(result);
            var returnValue = Assert.IsType<User>(result.Value);
            Assert.Equal(newUser.Id, returnValue.Id);
        }

        [Fact]
        public async Task UpdateUser_ShouldReturnNoContent_WhenUserIsUpdated()
        {
            var userId = Guid.NewGuid();
            var updatedUser = new User { Id = userId, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            _userRepositoryMock.Setup(repo => repo.UpdateAsync(updatedUser)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateUser(userId, updatedUser);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateUser_ShouldReturnBadRequest_WhenUserIdDoesNotMatch()
        {
            var userId = Guid.NewGuid();
            var updatedUser = new User { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };

            var result = await _controller.UpdateUser(userId, updatedUser);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnNoContent_WhenUserIsDeleted()
        {
            var userId = Guid.NewGuid();
            _userRepositoryMock.Setup(repo => repo.DeleteAsync(userId)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteUser(userId);

            Assert.IsType<NoContentResult>(result);
        }
    }
}