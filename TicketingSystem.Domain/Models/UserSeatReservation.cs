
using TicketingSystem.Domain.Models;

public class UserSeatReservation
{
    public Guid UserId { get; set; }
    public Guid SeatId { get; set; }

    public DateTime ReservedAt { get; set; }
    public DateTime ExpiresAt { get; set; }

    public User User { get; set; }
    public Seat Seat { get; set; }
}
