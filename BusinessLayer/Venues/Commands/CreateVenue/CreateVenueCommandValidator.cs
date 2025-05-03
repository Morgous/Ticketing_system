using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Venues.Commands.CreateVenue;

public class CreateVenueCommandValidator : AbstractValidator<CreateVenueCommand>
{
    private readonly ITicketingDbContext _context;

    public CreateVenueCommandValidator(ITicketingDbContext context)
    {

        _context = context;

        RuleFor(r => r.FullAddress)
            .NotEmpty()
            .MaximumLength(50)
            .MustAsync(BeUniqueName);

        RuleFor(r => r.Title)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(r => r.SectionCount)
            .NotEmpty()
            .Must(sc => sc > 0)
            .WithMessage("Section count must be above zero!");
    }

    public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return !await _context.Sections
            .AnyAsync(r => r.Name == name, cancellationToken);
    }
}
