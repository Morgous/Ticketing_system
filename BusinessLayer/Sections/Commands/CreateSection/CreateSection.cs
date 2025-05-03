using Ticketing_system.DomainLayer.Entities;
using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Sections.Commands.CreateSection;

public record CreateSectionCommand : IRequest<int>
{
    public required int VenueId { get; set; }

    public required string Name { get; set; }

    public required int RowCount { get; set; }
}

public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommand, int>
{
    private readonly ITicketingDbContext _context;

    public CreateSectionCommandHandler(ITicketingDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
    {
        var entity = new Section()
        {
            Name = request.Name,
            RowCount = request.RowCount,
            VenueId = request.VenueId
        };

        _context.Sections.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
