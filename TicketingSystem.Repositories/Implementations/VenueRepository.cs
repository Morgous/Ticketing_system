using Microsoft.EntityFrameworkCore;
using TicketingSystem.Infrastructure.Context;
using TicketingSystem.Repositories.Interfaces;

namespace TicketingSystem.Repositories.Implementations
{
    public class VenueRepository : GenericRepository<Venue>, IVenueRepository
    {
        private readonly AppDbContext _context;

        public VenueRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Venue>> GetVenuesByNameAsync(string name)
        {
            return await _context.Venues
                                 .Where(v => v.Name.Contains(name))
                                 .ToListAsync();
        }
    }
}
