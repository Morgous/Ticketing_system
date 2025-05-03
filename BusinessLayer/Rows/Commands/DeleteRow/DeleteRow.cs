using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Rows.Commands.DeleteRow;

public record DeleteRowCommand(int Id) : IRequest;


public class DeleteRowCommandHandler : IRequestHandler<DeleteRowCommand>
{
    private readonly ITicketingDbContext _context;

    public DeleteRowCommandHandler(ITicketingDbContext context)
    {
        _context = context;
    }


    public async Task Handle(DeleteRowCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Rows
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Rows.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
