using Microsoft.Extensions.Caching.Distributed;
using TicketingSystem.Repositories.Interfaces;

namespace TicketingService.Application.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDistributedCache _cache;

        public OrderService(IOrderRepository orderRepository, IDistributedCache cache)
        {
            _orderRepository = orderRepository;
            _cache = cache;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(Guid id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task AddOrderAsync(Order newOrder)
        {
            await _orderRepository.AddAsync(newOrder);
        }

        public async Task UpdateOrderAsync(Order updatedOrder)
        {
            await _orderRepository.UpdateAsync(updatedOrder);
        }

        public async Task DeleteOrderAsync(Guid id)
        {
            await _orderRepository.DeleteAsync(id);
        }

        public async Task InvalidateEventCache(Guid eventId)
        {
            await _cache.RemoveAsync($"Event_{eventId}");
        }
        public async Task<bool> TryBookSeatPessimisticAsync(Guid eventId, Guid seatId)
        {
            return await _orderRepository.TryBookSeatPessimisticAsync(eventId, seatId);
        }

        public async Task<bool> TryBookSeatOptimisticAsync(Guid eventId, Guid seatId,int version)
        {
            return await _orderRepository.TryBookSeatOptimisticAsync(eventId, seatId,version);
        }
    }
}