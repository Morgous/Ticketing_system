using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Seats.Commands.DeleteSeat;

public record DeleteSeatCommand(int Id) : IRequest;

public class DeleteSeatCommandHandler : IRequestHandler<DeleteSeatCommand>
{
    private readonly ITicketingDbContext _context;

    public DeleteSeatCommandHandler(ITicketingDbContext context)
    {
        _context = context;
    }


    public async Task Handle(DeleteSeatCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Seats
            .Where(s => s.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Seats.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
