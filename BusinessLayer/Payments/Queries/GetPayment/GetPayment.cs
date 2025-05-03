using Ticketing_system.DomainLayer.Enums;
using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Payments.Queries.GetPayment;

public record GetPaymentRequest(int PaymentId) : IRequest<PaymentState>;

public class GetPaymentRequestHandler : IRequestHandler<GetPaymentRequest, PaymentState>
{
    private readonly ITicketingDbContext _context;

    public GetPaymentRequestHandler(ITicketingDbContext context)
    {
        _context = context;
    }

    public async Task<PaymentState> Handle(GetPaymentRequest request, CancellationToken cancellationToken)
    {
        var payment = await _context.Payment
            .FirstOrDefaultAsync(p => p.Id == request.PaymentId, cancellationToken);

        Guard.Against.NotFound(request.PaymentId, payment);

        return payment.State;
    }
}
