using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Seats.Commands.UpdateSeat;

public record UpdateSeatCommand : IRequest
{
    public required int Id { get; set; }

    public required int RowId { get; set; }

    public required int PriceId { get; set; }

    public required string Name { get; set; }
}

public class UpdateSeatCommandHandler : IRequestHandler<UpdateSeatCommand>
{
    private readonly ITicketingDbContext _context;

    public UpdateSeatCommandHandler(ITicketingDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateSeatCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Seats.FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.Name;
        entity.PriceId = request.PriceId;
        entity.RowId = request.RowId;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
