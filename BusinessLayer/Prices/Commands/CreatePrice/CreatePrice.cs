using Ticketing_system.DomainLayer.Entities;
using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Prices.Commands.CreatePrice;

public record CreatePriceCommand : IRequest<int>
{
    public required decimal AmountToPay { get; set; }
}

public class CreateEventCommandHandler : IRequestHandler<CreatePriceCommand, int>
{
    private readonly ITicketingDbContext _context;

    public CreateEventCommandHandler(ITicketingDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreatePriceCommand request, CancellationToken cancellationToken)
    {
        var entity = new Price()
        {
            AmountToPay = request.AmountToPay
        };

        _context.Prices.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
