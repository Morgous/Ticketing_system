namespace TicketingSystem.Repositories.Interfaces
{
    public interface IVenueRepository : IGenericRepository<Venue>
    {
        Task<IEnumerable<Venue>> GetVenuesByNameAsync(string name);
    }
}
