
public class Section
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public Guid VenueId { get; set; }
    public Venue Venue { get; set; }

    public ICollection<Seat> Seats { get; set; } = new List<Seat>();
}
