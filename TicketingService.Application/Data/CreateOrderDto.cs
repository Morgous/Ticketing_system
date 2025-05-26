namespace TicketingService.Application.Data
{
    public class CreateOrderDto
    {
        public Guid CartId { get; set; }
        public Guid EventId { get; set; }
        public Guid SeatId { get; set; }
        public Guid PriceId { get; set; }
        public Guid UserId { get; set; }
        public int Version { get; set; }
    }
}
