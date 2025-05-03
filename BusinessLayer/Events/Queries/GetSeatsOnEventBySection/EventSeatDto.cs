using Ticketing_system.DomainLayer.Enums;

namespace Ticketing_System.BusinessLayer.Events.Queries.GetSeatsOnEventBySection;

public class EventSeatDto
{
    public required int Id { get; set; }

    public required int SectionId { get; set; }

    public required int RowId { get; set; }

    public required int PriceId { get; set; }

    public required int SeatId { get; set; }

    public required string SeatName { get; set; }

    public required decimal AmountToPay { get; set; }

    public required EventSeatState State { get; set; }
}
