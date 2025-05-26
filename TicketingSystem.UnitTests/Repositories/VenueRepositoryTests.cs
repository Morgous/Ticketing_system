using Moq;
using TicketingSystem.Repositories.Interfaces;
using Xunit;

namespace TicketingSystem.UnitTests.Repositories
{
    public class VenueRepositoryTests
    {
        private readonly Mock<IRepository<Venue>> _venueRepositoryMock;

        public VenueRepositoryTests()
        {
            _venueRepositoryMock = new Mock<IRepository<Venue>>();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfVenues()
        {
            var venues = new List<Venue> { new Venue { Id = Guid.NewGuid(), Name = "Venue 1", Address = "Address 1" } };
            _venueRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(venues);
            var result = await _venueRepositoryMock.Object.GetAllAsync();

            Assert.NotNull(result);
            Assert.IsType<List<Venue>>(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnVenue_WhenVenueExists()
        {
            var venueId = Guid.NewGuid();
            var venue = new Venue { Id = venueId, Name = "Venue 1", Address = "Address 1" };
            _venueRepositoryMock.Setup(repo => repo.GetByIdAsync(venueId)).ReturnsAsync(venue);

            var result = await _venueRepositoryMock.Object.GetByIdAsync(venueId);

            Assert.NotNull(result);
            Assert.IsType<Venue>(result);
            Assert.Equal(venueId, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenVenueDoesNotExist()
        {
            var venueId = Guid.NewGuid();
            _venueRepositoryMock.Setup(repo => repo.GetByIdAsync(venueId)).ReturnsAsync((Venue)null);

            var result = await _venueRepositoryMock.Object.GetByIdAsync(venueId);

            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_ShouldAddVenue()
        {
            var newVenue = new Venue { Id = Guid.NewGuid(), Name = "New Venue", Address = "New Address" };
            _venueRepositoryMock.Setup(repo => repo.AddAsync(newVenue)).Returns(Task.CompletedTask);

            await _venueRepositoryMock.Object.AddAsync(newVenue);

            _venueRepositoryMock.Verify(repo => repo.AddAsync(newVenue), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateVenue()
        {
            var updatedVenue = new Venue { Id = Guid.NewGuid(), Name = "My awesome Venue", Address = "Updated Address" };
            _venueRepositoryMock.Setup(repo => repo.UpdateAsync(updatedVenue)).Returns(Task.CompletedTask);

            await _venueRepositoryMock.Object.UpdateAsync(updatedVenue);

            _venueRepositoryMock.Verify(repo => repo.UpdateAsync(updatedVenue), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteVenue()
        {
            var venueId = Guid.NewGuid();
            _venueRepositoryMock.Setup(repo => repo.DeleteAsync(venueId)).Returns(Task.CompletedTask);

            await _venueRepositoryMock.Object.DeleteAsync(venueId);

            _venueRepositoryMock.Verify(repo => repo.DeleteAsync(venueId), Times.Once);
        }
    }
}