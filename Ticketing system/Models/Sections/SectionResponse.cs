using AutoMapper;
using Ticketing_System.BusinessLayer.Sections.Queries.GetSections;

namespace Ticketing_system.Models.Sections;

public class SectionResponse
{
    public required int Id { get; set; }

    public required int VenueId { get; set; }

    public required string Name { get; set; }

    public required int RowCount { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<SectionDto, SectionResponse>();
        }
    }
}
