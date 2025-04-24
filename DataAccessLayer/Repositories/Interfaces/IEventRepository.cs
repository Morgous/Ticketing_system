using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<IEnumerable<Event>> GetEvents(DateTime fromDate);
        Task<IEnumerable<Event>> GetEventsByVenueId(int venueId);
    }
}
