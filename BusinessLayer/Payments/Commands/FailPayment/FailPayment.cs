using Ticketing_system.DomainLayer.Enums;
using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Payments.Commands.FailPayment;

public record FailPaymentCommand(int PaymentId) : IRequest;

public class FailPaymentCommandHandler : IRequestHandler<FailPaymentCommand>
{
    private readonly ITicketingDbContext _context;

    public FailPaymentCommandHandler(ITicketingDbContext context)
    {
        _context = context;
    }

    public async Task Handle(FailPaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await _context.Payment
            .Include(p => p.EventSeats)
            .FirstOrDefaultAsync(p => p.Id == request.PaymentId, cancellationToken);

        Guard.Against.NotFound(request.PaymentId, payment);

        payment.State = PaymentState.Failed;

        foreach (var eventSeat in payment.EventSeats)
        {
            eventSeat.State = EventSeatState.Available;
        }

        _context.Payment.Update(payment);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
