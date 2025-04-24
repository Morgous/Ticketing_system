using BusinessLayer.Entities;

namespace BusinessLayer.Services.Interfaces
{
    public interface IEventService
    {
        bool CreateEvent(Event obj);

        Task<IEnumerable<Event>> GetAllEventsAsync();
    }
}
