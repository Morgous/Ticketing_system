using Ticketing_system.DomainLayer.Entities;
using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Venues.Commands.CreateVenue;

public record CreateVenueCommand : IRequest<int>
{
    public required string Title { get; set; }

    public required string FullAddress { get; set; }

    public required int SectionCount { get; set; }
}

public class CreateVenueCommandHandler : IRequestHandler<CreateVenueCommand, int>
{
    private readonly ITicketingDbContext _context;

    public CreateVenueCommandHandler(ITicketingDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateVenueCommand request, CancellationToken cancellationToken)
    {
        var entity = new Venue()
        {
            FullAddress = request.FullAddress,
            SectionCount = request.SectionCount,
            Title = request.Title
        };

        _context.Venues.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
