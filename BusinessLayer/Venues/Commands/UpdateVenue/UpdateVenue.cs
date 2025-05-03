using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Venues.Commands.UpdateVenue;

public record UpdateVenueCommand : IRequest
{
    public required int Id { get; set; }

    public required string Title { get; set; }

    public required string FullAddress { get; set; }

    public required int SectionCount { get; set; }
}

public class UpdateVenueCommandHandler : IRequestHandler<UpdateVenueCommand>
{
    private readonly ITicketingDbContext _context;

    public UpdateVenueCommandHandler(ITicketingDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateVenueCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Venues.FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.FullAddress = request.FullAddress;
        entity.SectionCount = request.SectionCount;
        entity.Title = request.Title;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
