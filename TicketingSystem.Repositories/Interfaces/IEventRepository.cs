namespace TicketingSystem.Repositories.Interfaces
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        Task<IEnumerable<Event>> GetEventsByLocationAsync(string location);
    }
}
