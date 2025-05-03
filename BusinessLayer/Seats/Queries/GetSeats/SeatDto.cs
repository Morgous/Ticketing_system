using Ticketing_system.DomainLayer.Entities;

namespace Ticketing_System.BusinessLayer.Seats.Queries.GetSeats;

public class SeatDto
{
    public required int Id { get; set; }

    public required int RowId { get; set; }

    public required int PriceId { get; set; }

    public required string Name { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Seat, SeatDto>();
        }
    }
}
