using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Sections.Commands.UpdateSection;

public class UpdateSectionCommandValidator : AbstractValidator<UpdateSectionCommand>
{
    private readonly ITicketingDbContext _context;

    public UpdateSectionCommandValidator(ITicketingDbContext context)
    {
        _context = context;

        RuleFor(r => r.Name)
            .NotEmpty()
            .MaximumLength(20)
            .MustAsync(BeUniqueName);

        RuleFor(r => r.VenueId)
            .NotEmpty()
            .MustAsync(VenueExists)
            .WithMessage("Venue must exist!");

        RuleFor(r => r.RowCount)
            .NotEmpty()
            .Must(rc => rc > 0)
            .WithMessage("Row count must be above zero!");
    }

    public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return !await _context.Sections
            .AnyAsync(r => r.Name == name, cancellationToken);
    }

    public async Task<bool> VenueExists(int venueId, CancellationToken cancellationToken)
    {
        return !await _context.Venues
            .AnyAsync(s => s.Id == venueId, cancellationToken);
    }
}
