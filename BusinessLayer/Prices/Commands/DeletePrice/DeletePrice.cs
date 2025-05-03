using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Prices.Commands.DeletePrice;

public record DeletePriceCommand(int Id) : IRequest;

public class DeletePriceCommandHandler : IRequestHandler<DeletePriceCommand>
{
    private readonly ITicketingDbContext _context;

    public DeletePriceCommandHandler(ITicketingDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeletePriceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Prices
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Prices.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
