using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Prices.Commands.UpdatePrice;

public record UpdatePriceCommand : IRequest
{
    public required int Id { get; set; }

    public required decimal AmountToPay { get; set; }
}

internal class UpdatePriceCommandHandler : IRequestHandler<UpdatePriceCommand>
{
    private readonly ITicketingDbContext _context;

    public UpdatePriceCommandHandler(ITicketingDbContext context)
    {
        _context = context;
    }


    public async Task Handle(UpdatePriceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Prices.FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.AmountToPay = request.AmountToPay;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
