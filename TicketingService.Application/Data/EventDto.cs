namespace TicketingSystem.Application.Data
{
    public class EventDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime EventDate { get; set; }
    }
}
