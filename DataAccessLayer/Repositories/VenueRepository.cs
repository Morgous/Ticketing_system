using DataAccessLayer.Entities;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class VenueRepository : Repository<Venue>, IVenueRepository
    {
        public VenueRepository(TicketingSystemDbContext context) : base(context) { }

        public async Task<Venue> GetVenue(int venueId)
        {
            return await DbSet
                .Include(v => v.VenueSections.Select(s => s.VenueRows.Select(r => r.VenueSeats)))
                .FirstOrDefaultAsync(v => v.Id == venueId);
        }

        public async Task<IEnumerable<VenueSeat>> GetAvailableSeats(int eventId, int venueId)
        {
            var allSeats = await Context.VenueSeats
                .Where(s => s.Row.Section.VenueId == venueId)
                .ToListAsync();

            var bookedSeatIds = await Context.EventSeats
                .Where(es => es.EventId == eventId)
                .Select(es => es.SeatId)
                .ToListAsync();

            return allSeats.Where(s => !bookedSeatIds.Contains(s.Id));
        }
    }
}
