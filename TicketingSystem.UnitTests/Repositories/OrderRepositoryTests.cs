using Moq;
using TicketingSystem.Repositories.Interfaces;
using Xunit;

namespace TicketingSystem.UnitTests.Repositories
{
    public class OrderRepositoryTests
    {
        private readonly Mock<IRepository<Order>> _orderRepositoryMock;

        public OrderRepositoryTests()
        {
            _orderRepositoryMock = new Mock<IRepository<Order>>();
        }

        [Fact]
        public async Task GetOrderByIdAsync_ShouldReturnOrder_WhenOrderExists()
        {
            var orderId = Guid.NewGuid();
            var order = new Order { Id = orderId };
            _orderRepositoryMock.Setup(repo => repo.GetByIdAsync(orderId)).ReturnsAsync(order);

            var result = await _orderRepositoryMock.Object.GetByIdAsync(orderId);

            Assert.NotNull(result);
            Assert.Equal(orderId, result.Id);
        }

    }
}