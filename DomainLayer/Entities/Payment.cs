using Ticketing_system.DomainLayer.Enums;

namespace Ticketing_system.DomainLayer.Entities;

public class Payment : BaseEntity
{
    public required PaymentState State { get; set; }

    public List<EventSeat> EventSeats { get; set; } = [];
}
