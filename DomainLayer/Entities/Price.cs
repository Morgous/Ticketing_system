namespace Ticketing_system.DomainLayer.Entities;

public class Price : BaseEntity
{
    public required decimal AmountToPay { get;set; }

    public IList<Seat> Seats { get; set; } = new List<Seat>();
}
