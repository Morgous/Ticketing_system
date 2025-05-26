using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TicketingService.Application.Interfaces;
using TicketingService.Application.Services;
using TicketingSystem.API.Controllers;
using TicketingSystem.Domain.Enums;
using TicketingSystem.Repositories.Interfaces;
using Xunit;

namespace TicketingSystem.UnitTests.Controllers
{
    public class EventControllerTests
    {
        private readonly Mock<IRepository<Event>> _eventRepositoryMock;
        private readonly Mock<IRepository<Seat>> _seatRepositoryMock;
        private readonly EventService _eventService;
        private readonly EventController _controller;
		private readonly ICacheService _cache;
		private readonly IMapper _mapper;

		public EventControllerTests()
        {
            _eventRepositoryMock = new Mock<IRepository<Event>>();
            _seatRepositoryMock = new Mock<IRepository<Seat>>();
			_cache = new Mock<ICacheService>().Object;
			_mapper = new Mock<IMapper>().Object;

			_eventService = new EventService(_eventRepositoryMock.Object, _seatRepositoryMock.Object, _cache);

            _controller = new EventController(_eventService, _cache, _mapper);
        }

        [Fact]
        public async Task GetAllEvents_ShouldReturnOkResult_WithListOfEvents()
        {
            var events = new List<Event> { new Event { Id = Guid.NewGuid(), Title = "Event 1", Location = "Location 1", EventDate = DateTime.Now } };
            _eventRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(events);

            var result = await _controller.GetAllEvents() as OkObjectResult;

            Assert.NotNull(result);
            var returnValue = Assert.IsType<List<Event>>(result.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetEventById_ShouldReturnOkResult_WhenEventExists()
        {
            var eventId = Guid.NewGuid();
            var eventItem = new Event { Id = eventId, Title = "Event 1", Location = "Location 1", EventDate = DateTime.Now };
            _eventRepositoryMock.Setup(repo => repo.GetByIdAsync(eventId)).ReturnsAsync(eventItem);

            var result = await _controller.GetEventById(eventId) as OkObjectResult;

            Assert.NotNull(result);
            var returnValue = Assert.IsType<Event>(result.Value);
            Assert.Equal(eventId, returnValue.Id);
        }

        [Fact]
        public async Task GetEventById_ShouldReturnNotFound_WhenEventDoesNotExist()
        {
            var eventId = Guid.NewGuid();
            _eventRepositoryMock.Setup(repo => repo.GetByIdAsync(eventId)).ReturnsAsync((Event)null);

            var result = await _controller.GetEventById(eventId);

            Assert.IsType<NotFoundResult>(result);
        }

        //[Fact]
        //public async Task CreateEvent_ShouldReturnCreatedAtActionResult()
        //{
        //    var newEvent = new CreateEventDto { Id = Guid.NewGuid(), Title = "New Event", Location = "New Location", EventDate = DateTime.Now };
        //    _eventRepositoryMock.Setup(repo => repo.AddAsync(newEvent)).Returns(Task.CompletedTask);

        //    var result = await _controller.CreateEvent(newEvent) as CreatedAtActionResult;

        //    Assert.NotNull(result);
        //    var returnValue = Assert.IsType<Event>(result.Value);
        //    Assert.Equal(newEvent.Id, returnValue.Id);
        //}

        [Fact]
        public async Task UpdateEvent_ShouldReturnNoContent_WhenEventIsUpdated()
        {
            var eventId = Guid.NewGuid();
            var updatedEvent = new Event { Id = eventId, Title = "Updated Event", Location = "Updated Location", EventDate = DateTime.Now };
            _eventRepositoryMock.Setup(repo => repo.UpdateAsync(updatedEvent)).Returns(Task.CompletedTask);
            var result = await _controller.UpdateEvent(eventId, updatedEvent);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateEvent_ShouldReturnBadRequest_WhenEventIdDoesNotMatch()
        {
            var eventId = Guid.NewGuid();
            var updatedEvent = new Event { Id = Guid.NewGuid(), Title = "Updated Event", Location = "Updated Location", EventDate = DateTime.Now };
            var result = await _controller.UpdateEvent(eventId, updatedEvent);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteEvent_ShouldReturnNoContent_WhenEventIsDeleted()
        {
            var eventId = Guid.NewGuid();
            _eventRepositoryMock.Setup(repo => repo.DeleteAsync(eventId)).Returns(Task.CompletedTask);
            var result = await _controller.DeleteEvent(eventId);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetSeatsByEventAndSection_ShouldReturnOkResult_WithListOfSeats()
        {
            var eventId = Guid.NewGuid();
            var sectionId = Guid.NewGuid();
            var seats = new List<Seat> { new Seat { Id = Guid.NewGuid(), Row = "A", SeatNumber = "1", Status = SeatStatus.Available, EventId = eventId, SectionId = sectionId } };
            _seatRepositoryMock.Setup(repo => repo.FindAsync(seat => seat.EventId == eventId && seat.SectionId == sectionId)).ReturnsAsync(seats);

            var result = await _controller.GetSeatsByEventAndSection(eventId, sectionId) as OkObjectResult;

            Assert.NotNull(result);
            var returnValue = Assert.IsType<List<Seat>>(result.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetSeatsByEventAndSection_ShouldReturnNotFound_WhenNoSeatsExist()
        {
            var eventId = Guid.NewGuid();
            var sectionId = Guid.NewGuid();
            _seatRepositoryMock.Setup(repo => repo.FindAsync(seat => seat.EventId == eventId && seat.SectionId == sectionId)).ReturnsAsync((List<Seat>)null);
            var result = await _controller.GetSeatsByEventAndSection(eventId, sectionId);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}