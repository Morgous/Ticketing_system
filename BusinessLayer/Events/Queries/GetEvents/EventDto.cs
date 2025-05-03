using Ticketing_system.DomainLayer.Entities;

namespace Ticketing_System.BusinessLayer.Events.Queries.GetEvents;
public class EventDto
{
    public required int Id { get; set; }

    public required int VenueId { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public required DateTime EventDate { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Event, EventDto>();
        }
    }
}
