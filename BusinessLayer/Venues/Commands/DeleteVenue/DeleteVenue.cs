using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Venues.Commands.DeleteVenue;

public record DeleteVenueCommand(int Id) : IRequest;

public class DeleteVenueCommandHandler : IRequestHandler<DeleteVenueCommand>
{
    private readonly ITicketingDbContext _context;

    public DeleteVenueCommandHandler(ITicketingDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteVenueCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Venues
            .Where(v => v.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Venues.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
