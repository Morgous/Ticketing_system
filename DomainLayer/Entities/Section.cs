namespace Ticketing_system.DomainLayer.Entities;

public class Section : BaseEntity
{
    public required int VenueId { get; set; }

    public required string Name { get; set; }

    public required int RowCount { get; set; }

    public Venue Venue { get; set; } = null!;

    public IList<Row> Rows { get; set; } = new List<Row>();
}
