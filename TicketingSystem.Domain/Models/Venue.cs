
public class Venue
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }

    public ICollection<Section> Sections { get; set; } = new List<Section>();
}
