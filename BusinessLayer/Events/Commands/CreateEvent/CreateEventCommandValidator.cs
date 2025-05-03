using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Events.Commands.CreateEvent;
internal class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
    private readonly ITicketingDbContext _context;

    public CreateEventCommandValidator(ITicketingDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(50)
            .MustAsync(BeUniqueTitle)
            .WithMessage("'{PropertyName}' must be unique.")
            .WithErrorCode("Unique");
    }

    public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return !await _context.Events
            .AnyAsync(l => l.Title == title, cancellationToken);
    }
}
