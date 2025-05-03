using Ticketing_system.DomainLayer.Entities;
using Ticketing_system.DomainLayer.Enums;
using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Carts.Commands.UpdateSeatsInCart;

public record BookSeatsInCartCommand(Guid CartId) : IRequest<int>;

public class BookSeatsInCartCommandHandler : IRequestHandler<BookSeatsInCartCommand, int>
{
    private readonly ITicketingDbContext _context;

    public BookSeatsInCartCommandHandler(ITicketingDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(BookSeatsInCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await _context.Carts
            .Include(c => c.EventSeats)
            .FirstOrDefaultAsync(c => c.CartId == request.CartId, cancellationToken);

        Guard.Against.NotFound(request.CartId, cart);

        foreach (var eventSeat in cart.EventSeats)
        {
            eventSeat.State = EventSeatState.Booked;
        }

        _context.Carts.Update(cart);

        var payment = new Payment()
        {
            State = PaymentState.Created,
            EventSeats = cart.EventSeats
        };

        _context.Payment.Add(payment);

        await _context.SaveChangesAsync(cancellationToken);

        return payment.Id;
    }
}
