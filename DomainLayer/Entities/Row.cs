namespace Ticketing_system.DomainLayer.Entities;

public class Row : BaseEntity
{
    public required int SectionId { get; set; }

    public required string Name { get; set; }

    public required int SeatCount { get; set; }

    public Section Section { get; set; } = null!;

    public IList<Seat> Seats{ get; set; } = new List<Seat>();
}
