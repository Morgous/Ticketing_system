using Ticketing_system.DomainLayer.Entities;
using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Events.Commands.CreateEvent;

public record CreateEventCommand : IRequest<int>
{
    public required int VenueId { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public required DateTime EventDate { get; set; }
}

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, int>
{
    private readonly ITicketingDbContext _context;

    public CreateEventCommandHandler(ITicketingDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var entity = new Event()
        {
            Title = request.Title,
            Description = request.Description,
            EventDate = request.EventDate,
            VenueId = request.VenueId
        };

        _context.Events.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
