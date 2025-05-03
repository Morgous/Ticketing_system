using Ticketing_system.DomainLayer.Entities;

namespace Ticketing_System.BusinessLayer.Sections.Queries.GetSections;

public class SectionDto
{
    public required int Id { get; set; }

    public required int VenueId { get; set; }

    public required string Name { get; set; }

    public required int RowCount { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Section, SectionDto>();
        }
    }
}
