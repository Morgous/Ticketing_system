using TicketingSystem.Domain.Enums;

public class Payment
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public PaymentStatus Status { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
}
