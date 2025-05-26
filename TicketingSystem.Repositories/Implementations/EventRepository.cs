using Microsoft.EntityFrameworkCore;
using TicketingSystem.Infrastructure.Context;
using TicketingSystem.Repositories.Interfaces;

namespace TicketingSystem.Repositories.Implementations
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetEventsByLocationAsync(string location)
        {
            return await _context.Events
                                 .Where(e => e.Location.Contains(location))
                                 .ToListAsync();
        }
    }
}
