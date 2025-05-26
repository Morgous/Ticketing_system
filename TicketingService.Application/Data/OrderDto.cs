namespace TicketingSystem.Application.Data
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Guid EventId { get; set; }
        public Guid SeatId { get; set; }
        public Guid PriceId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
    }
}
