using Ticketing_system.DomainLayer.Entities;

namespace Ticketing_System.BusinessLayer.Venues.Queries.GetVenues;

public class VenueDto
{
    public required int Id { get; set; }

    public required string Title { get; set; }

    public required string FullAddress { get; set; }

    public required int SectionCount { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Venue, VenueDto>();
        }
    }
}
