using Microsoft.AspNetCore.Mvc;
using Moq;
using TicketingSystem.Api.Controllers;
using TicketingSystem.Application.Services;
using TicketingSystem.Repositories.Interfaces;
using Xunit;

namespace TicketingSystem.UnitTests.Controllers
{
    public class VenueControllerTests
    {
        private readonly Mock<IRepository<Venue>> _venueRepositoryMock;
        private readonly VenueService _venueService;
        private readonly VenueController _controller;

        public VenueControllerTests()
        {
            _venueRepositoryMock = new Mock<IRepository<Venue>>();
            _venueService = new VenueService(_venueRepositoryMock.Object);
            _controller = new VenueController(_venueService);
        }

        [Fact]
        public async Task GetAllVenues_ShouldReturnOkResult_WithListOfVenues()
        {
            var venues = new List<Venue> { new Venue { Id = Guid.NewGuid(), Name = "Venue 1", Address = "Address 1" } };
            _venueRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(venues);

            var result = await _controller.GetAllVenues() as OkObjectResult;

            Assert.NotNull(result);
            var returnValue = Assert.IsType<List<Venue>>(result.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetVenueById_ShouldReturnOkResult_WhenVenueExists()
        {
            var venueId = Guid.NewGuid();
            var venue = new Venue { Id = venueId, Name = "Venue 1", Address = "Address 1" };
            _venueRepositoryMock.Setup(repo => repo.GetByIdAsync(venueId)).ReturnsAsync(venue);

            var result = await _controller.GetVenueById(venueId) as OkObjectResult;

            Assert.NotNull(result);
            var returnValue = Assert.IsType<Venue>(result.Value);
            Assert.Equal(venueId, returnValue.Id);
        }

        [Fact]
        public async Task GetVenueById_ShouldReturnNotFound_WhenVenueDoesNotExist()
        {
            var venueId = Guid.NewGuid();
            _venueRepositoryMock.Setup(repo => repo.GetByIdAsync(venueId)).ReturnsAsync((Venue)null);

            var result = await _controller.GetVenueById(venueId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateVenue_ShouldReturnCreatedAtActionResult()
        {
            var newVenue = new Venue { Id = Guid.NewGuid(), Name = "New Venue", Address = "New Address" };

            _venueRepositoryMock.Setup(repo => repo.AddAsync(newVenue)).Returns(Task.CompletedTask);

            var result = await _controller.CreateVenue(newVenue) as CreatedAtActionResult;

            var returnValue = Assert.IsType<Venue>(result.Value);
            Assert.Equal(newVenue.Id, returnValue.Id);
        }

        [Fact]
        public async Task UpdateVenue_ShouldReturnNoContent_WhenVenueIsUpdated()
        {
            var venueId = Guid.NewGuid();
            var updatedVenue = new Venue { Id = venueId, Name = "My awesome Venue", Address = "Updated Address" };
            _venueRepositoryMock.Setup(repo => repo.UpdateAsync(updatedVenue)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateVenue(venueId, updatedVenue);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateVenue_ShouldReturnBadRequest_WhenVenueIdDoesNotMatch()
        {
            var venueId = Guid.NewGuid();
            var updatedVenue = new Venue { Id = Guid.NewGuid(), Name = "My awesome Venue", Address = "Updated Address" };

            var result = await _controller.UpdateVenue(venueId, updatedVenue);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteVenue_ShouldReturnNoContent_WhenVenueIsDeleted()
        {
            var venueId = Guid.NewGuid();
            _venueRepositoryMock.Setup(repo => repo.DeleteAsync(venueId)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteVenue(venueId);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetSectionsByVenueId_ShouldReturnOkResult_WithListOfSections()
        {
            var venueId = Guid.NewGuid();
            var sections = new List<Section> { new Section { Id = Guid.NewGuid(), Name = "Section 1" } };
            var venue = new Venue { Id = venueId, Name = "Venue 1", Address = "Address 1", Sections = sections };
            _venueRepositoryMock.Setup(repo => repo.GetByIdAsync(venueId)).ReturnsAsync(venue);

            var result = await _controller.GetSectionsByVenueId(venueId) as OkObjectResult;

            Assert.NotNull(result);
            var returnValue = Assert.IsType<List<Section>>(result.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetSectionsByVenueId_ShouldReturnNotFound_WhenVenueDoesNotExist()
        {
            var venueId = Guid.NewGuid();
            _venueRepositoryMock.Setup(repo => repo.GetByIdAsync(venueId)).ReturnsAsync((Venue)null);

            var result = await _controller.GetSectionsByVenueId(venueId);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}