using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Rows.Commands.UpdateRow;

public record UpdateRowCommand : IRequest
{
    public required int Id { get; set; }

    public required int SectionId { get; set; }

    public required string Name { get; set; }

    public required int SeatCount { get; set; }
}

public class UpdateRowCommandHandler : IRequestHandler<UpdateRowCommand>
{
    private readonly ITicketingDbContext _context;

    public UpdateRowCommandHandler(ITicketingDbContext context)
    {
        _context = context;
    }


    public async Task Handle(UpdateRowCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Rows.FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.Name;
        entity.SectionId = request.SectionId;
        entity.SeatCount = request.SeatCount;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
