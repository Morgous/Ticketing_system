using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Rows.Commands.UpdateRow;

public class UpdateRowCommandValidator : AbstractValidator<UpdateRowCommand>
{
    private readonly ITicketingDbContext _context;

    public UpdateRowCommandValidator(ITicketingDbContext context)
    {
        _context = context;

        RuleFor(r => r.Name)
            .NotEmpty()
            .MaximumLength(20)
            .MustAsync(BeUniqueName);

        RuleFor(r => r.SeatCount)
            .NotEmpty()
            .Must(sc => sc > 0)
            .WithMessage("Seat count must be above zero!");

        RuleFor(r => r.SectionId)
            .NotEmpty()
            .MustAsync(SectionExists).
            WithMessage("Section must exist!");
    }

    public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return !await _context.Rows
            .AnyAsync(r => r.Name == name, cancellationToken);
    }

    public async Task<bool> SectionExists(int sectionId, CancellationToken cancellationToken)
    {
        return !await _context.Sections
            .AnyAsync(s => s.Id == sectionId, cancellationToken);
    }
}
