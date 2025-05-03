using AutoMapper;
using Ticketing_System.BusinessLayer.Venues.Queries.GetVenues;

namespace Ticketing_system.Models.Venues;

public class VenuesResponse
{
    public required int Id { get; set; }

    public required string Title { get; set; }

    public required string FullAddress { get; set; }

    public required int SectionCount { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<VenueDto, VenuesResponse>(MemberList.Destination);
        }
    }
}
