namespace Ticketing_system.DomainLayer.Entities;

public class Event : BaseEntity
{
    public required int VenueId { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public required DateTime EventDate { get; set; }

    public Venue Venue { get; set; } = null!;

    public List<Seat> Seats { get; } = [];

    public List<EventSeat> EventSeats { get; } = [];
}
