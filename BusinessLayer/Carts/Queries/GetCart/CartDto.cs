using Ticketing_System.BusinessLayer.Events.Queries.GetSeatsOnEventBySection;

namespace Ticketing_System.BusinessLayer.Carts.Queries.GetCart;

public class CartDto
{
    public int Id { get; set; }

    public required Guid CartId { get; set; }

    public decimal TotalAmountToPay { get; set; }

    public List<EventSeatDto> EventSeats { get; set; } = [];
}
