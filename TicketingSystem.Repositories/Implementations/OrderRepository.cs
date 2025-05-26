using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TicketingSystem.Infrastructure.Context;
using TicketingSystem.Repositories.Interfaces;
using TicketingSystem.Domain.Enums;

namespace TicketingSystem.Repositories.Implementations
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository, IConcurrencyRepository<Order>
    {
        private readonly AppDbContext _context;
        private readonly IRepository<Seat> _seatRepository;

        public OrderRepository(AppDbContext context, IRepository<Seat> seatRepository) : base(context)
        {
            _context = context;
            _seatRepository = seatRepository;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId)
        {
            return await _context.Orders
                                 .Where(o => o.UserId == userId)
                                 .ToListAsync();
        }

        public async Task<bool> TryBookSeatPessimisticAsync(Guid eventId, Guid seatId)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                var seat = await _context.Seats
                    .FromSqlRaw("SELECT * FROM Seats WITH (UPDLOCK) WHERE EventId = {0} AND Id = {1}", eventId, seatId)
                    .FirstOrDefaultAsync();

                if (seat == null || seat.Status == SeatStatus.Sold)
                {
                    return false;
                }

                seat.Status = SeatStatus.Sold;
                _context.Seats.Update(seat);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }

            return true;
        }

        public async Task<bool> TryBookSeatOptimisticAsync(Guid eventId, Guid seatId, int originalVersion)
        {
            var seat = await _context.Seats.FirstOrDefaultAsync(s => s.EventId == eventId && s.Id == seatId);
            if (seat == null || seat.Status == SeatStatus.Sold)
            {
                return false;
            }

            var order = await _context.Orders.FirstOrDefaultAsync(o => o.EventId == eventId && o.SeatId == seatId && o.Status!=OrderStatus.Booked);
            if (order == null)
            {
                order = new Order
                {
                    Id = Guid.NewGuid(),
                    EventId = eventId,
                    SeatId = seatId,
                    Status = OrderStatus.Pending,
                    CreatedAt = DateTime.UtcNow,
                    Version = originalVersion
                };
                _context.Orders.Add(order);
            }
            else
            {
                if (order.Status == OrderStatus.Booked)
                {
                    return false;
                }

                _context.Entry(order).OriginalValues["Version"] = originalVersion;

                order.Status = OrderStatus.Booked;
                order.Version++; 
            }

            seat.Status = SeatStatus.Sold;

            try
            {
                _context.Seats.Update(seat);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(System.Data.IsolationLevel isolationLevel)
        {
            return await _context.Database.BeginTransactionAsync(isolationLevel);
        }

        public void SetOriginalVersion(Order entity, int originalVersion)
        {
            _context.Entry(entity).OriginalValues["Version"] = originalVersion;
        }

    }
}