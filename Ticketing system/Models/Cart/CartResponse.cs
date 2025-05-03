using AutoMapper;
using Ticketing_system.Models.EventSeat;
using Ticketing_System.BusinessLayer.Carts.Queries.GetCart;

namespace Ticketing_system.Models.Cart;

public class CartResponse
{
    public int Id { get; set; }

    public required Guid CartId { get; set; }

    public decimal TotalAmountToPay { get; set; }

    public List<EventSeatResponse> EventSeats { get; set; } = [];

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CartDto, CartResponse>(MemberList.Destination);
        }
    }
}
