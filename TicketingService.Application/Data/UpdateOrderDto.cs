using TicketingSystem.Domain.Enums;

namespace TicketingService.Application.Data
{
    public class UpdateOrderDto
    {
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
        public Guid? PaymentId { get; set; }
    }
}
