namespace Ticketing_system.DomainLayer.Entities;

public class Seat : BaseEntity
{
    public required int RowId { get; set; }

    public required int PriceId { get; set; }

    public required string Name { get; set; }

    public Row Row { get; set; } = null!;

    public Price Price { get; set; } = null!;

    public List<Event> Events { get; } = [];

    public List<EventSeat> EventSeats { get; } = [];
}
