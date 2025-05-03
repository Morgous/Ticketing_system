using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Events.Commands.DeleteEvent;

public record DeleteEventCommand(int Id) : IRequest;

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
{
    private readonly ITicketingDbContext _context;

    public DeleteEventCommandHandler(ITicketingDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Events
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Events.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
