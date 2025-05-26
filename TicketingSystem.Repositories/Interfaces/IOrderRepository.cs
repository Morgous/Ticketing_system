namespace TicketingSystem.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>, IConcurrencyRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId);
        Task<bool> TryBookSeatPessimisticAsync(Guid eventId, Guid seatId);
        Task<bool> TryBookSeatOptimisticAsync(Guid eventId, Guid seatId, int version);
    }
}
