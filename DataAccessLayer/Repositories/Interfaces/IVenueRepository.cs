using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IVenueRepository : IRepository<Venue>
    {
        Task<Venue> GetVenue(int venueId);
        Task<IEnumerable<VenueSeat>> GetAvailableSeats(int eventId, int venueId);
    }

}
