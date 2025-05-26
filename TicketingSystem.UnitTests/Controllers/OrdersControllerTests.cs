using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using TicketingService.Application.Services;
using TicketingSystem.API.Controllers;
using TicketingSystem.Repositories.Interfaces;
using Xunit;

namespace TicketingSystem.UnitTests.Controllers
{
    public class OrdersControllerTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly OrderService _orderService;
        private readonly OrderController _controller;
		private readonly IDistributedCache _cache;
		private readonly IRepository<Seat> _seatRepository;

		public OrdersControllerTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _mapperMock = new Mock<IMapper>();
			_cache = new Mock<IDistributedCache>().Object;

			_orderService = new OrderService(_orderRepositoryMock.Object, _cache);

            _controller = new OrderController(_orderService, _mapperMock.Object, _cache);
        }

        [Fact]
        public async Task GetOrderById_ShouldReturnOrder_WhenOrderExists()
        {
            var orderId = Guid.NewGuid();
            var order = new Order { Id = orderId };
            _orderRepositoryMock.Setup(repo => repo.GetByIdAsync(orderId)).ReturnsAsync(order);

            var result = await _controller.GetOrderById(orderId) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(order, result.Value);
        }
    }
}