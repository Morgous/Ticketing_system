namespace Ticketing_system.DomainLayer.Entities;
public class Cart : BaseEntity
{
    public required Guid CartId { get; set; }

    public List<EventSeat> EventSeats { get; set; } = [];
}
