using Ticketing_system.DomainLayer.Entities;

namespace Ticketing_System.BusinessLayer.Rows.Queries.GetRows;

public class RowDto
{
    public required int Id { get; set; }

    public required int SectionId { get; set; }

    public required string Name { get; set; }

    public required int SeatCount { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Row, RowDto>();
        }
    }
}
