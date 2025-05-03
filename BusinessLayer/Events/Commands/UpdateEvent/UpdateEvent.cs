using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Events.Commands.UpdateEvent;

public record UpdateEventCommand : IRequest
{
    public required int Id { get; set; }

    public required int VenueId { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public required DateTime EventDate { get; set; }
}

internal class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand>
{
    private readonly ITicketingDbContext _context;

    public UpdateEventCommandHandler(ITicketingDbContext context)
    {
        _context = context;
    }


    public async Task Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Events.FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Title = request.Title;
        entity.Description = request.Description;
        entity.VenueId = request.VenueId;
        entity.EventDate = request.EventDate;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
