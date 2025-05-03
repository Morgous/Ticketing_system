using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Carts.Commands.DeleteSeatFromCart;

public record DeleteSeatFromCartCommant( ) : IRequest
{
    public required Guid CartId { get; set; }

    public required int EventSeatId { get; set; }
}

public class DeleteSeatFromCartCommantHandler : IRequestHandler<DeleteSeatFromCartCommant>
{
    private readonly ITicketingDbContext _context;

    public DeleteSeatFromCartCommantHandler(ITicketingDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteSeatFromCartCommant request, CancellationToken cancellationToken)
    {
        var cart = await _context.Carts.Include(c => c.EventSeats)
            .FirstOrDefaultAsync(x => x.CartId == request.CartId);

        Guard.Against.NotFound(request.CartId, cart);

        cart.EventSeats.RemoveAll(x => x.Id == request.EventSeatId);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
