namespace TicketingSystem.Repositories.Interfaces
{
    public interface ISeatRepository : IGenericRepository<Seat>
    {
        Task<IEnumerable<Seat>> GetAvailableSeatsForEventAsync(Guid eventId);
    }
}
