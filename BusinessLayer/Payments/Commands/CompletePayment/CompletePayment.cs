using Ticketing_system.DomainLayer.Enums;
using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Payments.Commands.CompletePayment;

public record CompletePaymentCommand(int PaymentId) : IRequest;

public class CompletePaymentCommandHandler : IRequestHandler<CompletePaymentCommand>
{
    private readonly ITicketingDbContext _context;

    public CompletePaymentCommandHandler(ITicketingDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CompletePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await _context.Payment
            .Include(p => p.EventSeats)
            .FirstOrDefaultAsync(p => p.Id == request.PaymentId, cancellationToken);

        Guard.Against.NotFound(request.PaymentId, payment);

        payment.State = PaymentState.Successful;

        foreach (var eventSeat in payment.EventSeats)
        {
            eventSeat.State = EventSeatState.Sold;
        }

        _context.Payment.Update(payment);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
