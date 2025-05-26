public class Event
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Location { get; set; }
    public DateTime EventDate { get; set; }

    public ICollection<Seat> Seats { get; set; } = new List<Seat>();
}
