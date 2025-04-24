using DataAccessLayer.Entities;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(TicketingSystemDbContext context) : base(context) { }

        public async Task<IEnumerable<Event>> GetEvents(DateTime fromDate)
        {
            return await DbSet.Where(e => e.StartDate >= fromDate).ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetEventsByVenueId(int venueId)
        {
            return await DbSet.Where(e => e.EventVenues.Any(ev => ev.VenueId == venueId)).ToListAsync();
        }
    }
}
