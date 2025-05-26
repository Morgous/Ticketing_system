using Moq;
using TicketingSystem.Repositories.Interfaces;
using Xunit;

namespace TicketingSystem.UnitTests.Repositories
{
    public class EventRepositoryTests
    {
        private readonly Mock<IRepository<Event>> _eventRepositoryMock;

        public EventRepositoryTests()
        {
            _eventRepositoryMock = new Mock<IRepository<Event>>();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfEvents()
        {
            var events = new List<Event> { new Event { Id = Guid.NewGuid(), Title = "Event 1", Location = "Location 1", EventDate = DateTime.Now } };
            _eventRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(events);

            var result = await _eventRepositoryMock.Object.GetAllAsync();

            Assert.NotNull(result);
            Assert.IsType<List<Event>>(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEvent_WhenEventExists()
        {
            var eventId = Guid.NewGuid();
            var eventItem = new Event { Id = eventId, Title = "Event 1", Location = "Location 1", EventDate = DateTime.Now };
            _eventRepositoryMock.Setup(repo => repo.GetByIdAsync(eventId)).ReturnsAsync(eventItem);

            var result = await _eventRepositoryMock.Object.GetByIdAsync(eventId);

            Assert.NotNull(result);
            Assert.IsType<Event>(result);
            Assert.Equal(eventId, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenEventDoesNotExist()
        {
            var eventId = Guid.NewGuid();
            _eventRepositoryMock.Setup(repo => repo.GetByIdAsync(eventId)).ReturnsAsync((Event)null);

            var result = await _eventRepositoryMock.Object.GetByIdAsync(eventId);

            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_ShouldAddEvent()
        {
            var newEvent = new Event { Id = Guid.NewGuid(), Title = "New Event", Location = "New Location", EventDate = DateTime.Now };
            _eventRepositoryMock.Setup(repo => repo.AddAsync(newEvent)).Returns(Task.CompletedTask);

            await _eventRepositoryMock.Object.AddAsync(newEvent);

            _eventRepositoryMock.Verify(repo => repo.AddAsync(newEvent), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEvent()
        {
            var updatedEvent = new Event { Id = Guid.NewGuid(), Title = "Updated Event", Location = "Updated Location", EventDate = DateTime.Now };
            _eventRepositoryMock.Setup(repo => repo.UpdateAsync(updatedEvent)).Returns(Task.CompletedTask);

            await _eventRepositoryMock.Object.UpdateAsync(updatedEvent);

            _eventRepositoryMock.Verify(repo => repo.UpdateAsync(updatedEvent), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteEvent()
        {
            var eventId = Guid.NewGuid();
            _eventRepositoryMock.Setup(repo => repo.DeleteAsync(eventId)).Returns(Task.CompletedTask);

            await _eventRepositoryMock.Object.DeleteAsync(eventId);

            _eventRepositoryMock.Verify(repo => repo.DeleteAsync(eventId), Times.Once);
        }
    }
}