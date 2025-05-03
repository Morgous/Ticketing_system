using Ticketing_system.DomainLayer.Entities;
using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Carts.Commands.CreateSeatInCart;

public record CreateSeatInCartCommand : IRequest
{
    public required Guid CartId { get; set; }

    public required int EventSeatId { get; set; }
}

public class CreateSeatinCartCommandHandler : IRequestHandler<CreateSeatInCartCommand>
{
    private readonly ITicketingDbContext _context;

    public CreateSeatinCartCommandHandler(ITicketingDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateSeatInCartCommand request, CancellationToken cancellationToken)
    {
        var eventSeat = await _context.EventSeats.FirstOrDefaultAsync(x => x.Id == request.EventSeatId);

        Guard.Against.NotFound(request.EventSeatId, eventSeat);

        var cart = await _context.Carts.FirstOrDefaultAsync(c => c.CartId == request.CartId);

        if (cart == null)
        {
            await CreateCart(request.CartId, eventSeat, cancellationToken);
        }
        else
        {
            await UpdateCart(cart, eventSeat, cancellationToken);
        }
    }

    private async Task CreateCart(Guid CartId, EventSeat eventSeat, CancellationToken cancellationToken)
    {
        var cart = new Cart()
        {
            CartId = CartId,
            EventSeats =
            [
                eventSeat
            ]
        };

        _context.Carts.Add(cart);

        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task UpdateCart(Cart cart, EventSeat eventSeat, CancellationToken cancellationToken)
    {
        cart.EventSeats.Add(eventSeat);

        _context.Carts.Update(cart);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
