using Microsoft.EntityFrameworkCore;
using TicketingSystem.Infrastructure.Context;
using TicketingSystem.Repositories.Interfaces;

namespace TicketingSystem.Repositories.Implementations
{
    public class SeatRepository : GenericRepository<Seat>, ISeatRepository
    {
        private readonly AppDbContext _context;

        public SeatRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Seat>> GetAvailableSeatsForEventAsync(Guid eventId)
        {
            return await _context.Seats
                                 .Where(s => s.EventId == eventId && s.IsAvailable)
                                 .ToListAsync();
        }
    }
}
