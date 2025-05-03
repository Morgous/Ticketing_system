using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Sections.Commands.DeleteSection;

public record DeleteSectionCommand(int Id) : IRequest;

public class DeleteSectionCommandHandler : IRequestHandler<DeleteSectionCommand>
{
    private readonly ITicketingDbContext _context;

    public DeleteSectionCommandHandler(ITicketingDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Sections
            .Where(s => s.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Sections.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
