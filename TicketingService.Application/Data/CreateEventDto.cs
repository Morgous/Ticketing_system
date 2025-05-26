namespace TicketingService.Application.Data
{
    public class CreateEventDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime EventDate { get; set; }
        public List<CreateSeatDto> Seats { get; set; }
    }
}
