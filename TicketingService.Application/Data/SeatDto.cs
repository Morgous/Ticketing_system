namespace TicketingSystem.Application.Data
{
    public class SeatDto
    {
        public Guid Id { get; set; }
        public string Row { get; set; }
        public string SeatNumber { get; set; }
        public bool IsAvailable { get; set; }
        public Guid EventId { get; set; }
        public Guid SectionId { get; set; }
    }
}
