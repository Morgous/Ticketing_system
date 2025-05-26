using TicketingSystem.Domain.Enums;

namespace TicketingService.Application.Data
{
    public class CreateSeatDto
    {
        public Guid Id { get; set; }
        public string Row { get; set; }
        public string SeatNumber { get; set; }
        public bool IsAvailable { get; set; }
        public Guid SectionId { get; set; }
        public SeatStatus Status { get; set; }
    }
}
