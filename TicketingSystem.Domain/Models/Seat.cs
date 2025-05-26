
using TicketingSystem.Domain.Enums;

public class Seat
{
    public Guid Id { get; set; }
    public string Row { get; set; }
    public string SeatNumber { get; set; }
    public bool IsAvailable { get; set; }

    public Guid EventId { get; set; }
    public Event Event { get; set; }

    public Guid SectionId { get; set; }
    public Section Section { get; set; }

    public ICollection<UserSeatReservation> SeatReservations { get; set; } = new List<UserSeatReservation>();
    public SeatStatus Status { get; set; }

    public int Version { get; set; }
}
