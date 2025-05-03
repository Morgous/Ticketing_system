namespace Ticketing_system.DomainLayer.Entities;

public class Venue : BaseEntity
{
    public required string Title { get; set; }

    public required string FullAddress { get; set; }

    public required int SectionCount { get; set; }

    public IList<Event> Events { get; private set; } = new List<Event>();

    public IList<Section> Sections { get; private set; } = new List<Section>();
}
