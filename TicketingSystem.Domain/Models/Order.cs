using TicketingSystem.Domain.Enums;

public class Order
{
    public Guid Id { get; set; }
    public Guid CartId { get; set; }
    public Guid EventId { get; set; }
    public Guid SeatId { get; set; }
    public Guid PriceId { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid UserId { get; set; }
    public Guid? PaymentId { get; set; }
    public int Version { get; set; }
}
