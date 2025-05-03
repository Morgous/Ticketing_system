using Ticketing_System.BusinessLayer.Common.Interfaces;
using Ticketing_System.BusinessLayer.Events.Queries.GetSeatsOnEventBySection;

namespace Ticketing_System.BusinessLayer.Carts.Queries.GetCart;

public record GetCartRequest : IRequest<CartDto>
{
    public required Guid CartId { get; set; }
}

public class GetCartRequestHandler : IRequestHandler<GetCartRequest , CartDto>
{
    private readonly ITicketingDbContext _context;
    private readonly IMapper _mapper;

    public GetCartRequestHandler(ITicketingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CartDto> Handle(GetCartRequest request, CancellationToken cancellationToken)
    {
        var cart = await _context.Carts
            .AsNoTracking()
            .AsSplitQuery()
            .Select(c => new CartDto()
            {
                Id = c.Id,
                CartId = c.CartId,
                EventSeats = c.EventSeats.Select(es => new EventSeatDto()
                {
                    Id = es.Id,
                    SectionId = es.Seat.Row.SectionId,
                    AmountToPay = es.Seat.Price.AmountToPay,
                    SeatId = es.SeatId,
                    SeatName = es.Seat.Name,
                    State = es.State,
                    PriceId = es.Seat.PriceId,
                    RowId = es.Seat.RowId
                }).ToList(),
            })
            .FirstOrDefaultAsync(x => x.CartId == request.CartId, cancellationToken);

        Guard.Against.NotFound(request.CartId, cart);

        cart.TotalAmountToPay = cart.EventSeats.Sum(es => es.AmountToPay);

        return cart;
    }
}
