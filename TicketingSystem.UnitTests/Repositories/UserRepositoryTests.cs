using Moq;
using TicketingSystem.Domain.Models;
using TicketingSystem.Repositories.Interfaces;
using Xunit;

namespace TicketingSystem.UnitTests.Repositories
{
    public class UserRepositoryTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public UserRepositoryTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfUsers()
        {
            var users = new List<User> { new User { Id = Guid.NewGuid(), FirstName = "Jardani", LastName = "Jovonovich", Email = "Jardani.Jovonovich@example.com" } };
            _userRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);

            var result = await _userRepositoryMock.Object.GetAllAsync();

            Assert.NotNull(result);
            Assert.IsType<List<User>>(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, FirstName = "Jardani", LastName = "Jovonovich", Email = "Jardani.Jovonovich@example.com" };
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);

            var result = await _userRepositoryMock.Object.GetByIdAsync(userId);

            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userId, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            var userId = Guid.NewGuid();
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync((User)null);

            var result = await _userRepositoryMock.Object.GetByIdAsync(userId);

            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_ShouldAddUser()
        {
            var newUser = new User { Id = Guid.NewGuid(), FirstName = "Jardani", LastName = "Jovonovich", Email = "Jardani.Jovonovich@example.com" };
            _userRepositoryMock.Setup(repo => repo.AddAsync(newUser)).Returns(Task.CompletedTask);

            await _userRepositoryMock.Object.AddAsync(newUser);

            _userRepositoryMock.Verify(repo => repo.AddAsync(newUser), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateUser()
        {
            var updatedUser = new User { Id = Guid.NewGuid(), FirstName = "Jardani", LastName = "Jovonovich", Email = "Jardani.Jovonovich@example.com" };
            _userRepositoryMock.Setup(repo => repo.UpdateAsync(updatedUser)).Returns(Task.CompletedTask);

            await _userRepositoryMock.Object.UpdateAsync(updatedUser);

            _userRepositoryMock.Verify(repo => repo.UpdateAsync(updatedUser), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteUser()
        {
            var userId = Guid.NewGuid();
            _userRepositoryMock.Setup(repo => repo.DeleteAsync(userId)).Returns(Task.CompletedTask);

            await _userRepositoryMock.Object.DeleteAsync(userId);

            _userRepositoryMock.Verify(repo => repo.DeleteAsync(userId), Times.Once);
        }
    }
}