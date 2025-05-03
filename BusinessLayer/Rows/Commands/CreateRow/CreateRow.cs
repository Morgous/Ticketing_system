using Ticketing_system.DomainLayer.Entities;
using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Rows.Commands.CreateRow;

public record CreateRowCommand : IRequest<int>
{
    public required int SectionId { get; set; }

    public required string Name { get; set; }

    public required int SeatCount { get; set; }
}

public class CreateRowCommandHandler : IRequestHandler<CreateRowCommand , int>
{
    private readonly ITicketingDbContext _context;

    public CreateRowCommandHandler(ITicketingDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateRowCommand request, CancellationToken cancellationToken)
    {
        var entity = new Row()
        {
            Name = request.Name, 
            SectionId = request.SectionId, 
            SeatCount = request.SeatCount
        };

        _context.Rows.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
