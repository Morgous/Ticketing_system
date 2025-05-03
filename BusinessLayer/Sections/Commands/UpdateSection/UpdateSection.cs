using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Sections.Commands.UpdateSection;

public record UpdateSectionCommand : IRequest
{
    public required int Id { get; set; }

    public required int VenueId { get; set; }

    public required string Name { get; set; }

    public required int RowCount { get; set; }
}

public class UpdateSectionCommandHandler : IRequestHandler<UpdateSectionCommand>
{
    public readonly ITicketingDbContext _context;

    public UpdateSectionCommandHandler(ITicketingDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Sections.FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.Name;
        entity.RowCount = request.RowCount;
        entity.VenueId = request.VenueId;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
