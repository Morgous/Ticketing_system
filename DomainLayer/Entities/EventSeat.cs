using Ticketing_system.DomainLayer.Enums;

namespace Ticketing_system.DomainLayer.Entities;

public class EventSeat : BaseEntity
{
    public int EventId { get; set; }

    public int SeatId { get; set; }

    public EventSeatState State { get; set; }

    public Event Event { get; set; } = null!;

    public Seat Seat { get; set; } = null!;

    public List<Cart> Carts { get; } = [];

    public List<Payment> Payments { get; } = [];

}
