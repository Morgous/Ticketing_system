using Ticketing_system.DomainLayer.Entities;
using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Seats.Commands.CreateSeat;

public record CreateSeatCommand : IRequest<int>
{
    public required int RowId { get; set; }

    public required int PriceId { get; set; }

    public required string Name { get; set; }
}

internal class CreateSeatCommandHandler : IRequestHandler<CreateSeatCommand, int>
{
    private readonly ITicketingDbContext _context;

    public CreateSeatCommandHandler(ITicketingDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateSeatCommand request, CancellationToken cancellationToken)
    {
        var entity = new Seat()
        {
            Name = request.Name,
            PriceId = request.PriceId,
            RowId = request.RowId
        };

        _context.Seats.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
