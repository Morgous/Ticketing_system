using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Events.Commands.UpdateEvent;

internal class UpdatePriceCommandValidator : AbstractValidator<UpdateEventCommand>
{
    private readonly ITicketingDbContext _context;

    public UpdatePriceCommandValidator(ITicketingDbContext context)
    {
        _context = context;

        RuleFor(ev => ev.Title)
            .NotEmpty()
            .MaximumLength(50)
            .MustAsync(BeUniqueTitle)
            .WithMessage("'{PropertyName}' must be unique.")
            .WithErrorCode("Unique");

        RuleFor(ev => ev.Description)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(ev => ev)
            .Must(ev => ev.EventDate > DateTime.Today)
            .WithMessage("Event can't happen in the past!");

        RuleFor(ev => ev.VenueId)
            .NotEmpty()
            .MustAsync(VenueExists)
            .WithMessage("Venue you chose doesn't exist!");
    }

    public async Task<bool> BeUniqueTitle(UpdateEventCommand model, string title, CancellationToken cancellationToken)
    {
        return !await _context.Events
            .Where(l => l.Id != model.Id)
            .AnyAsync(l => l.Title == title, cancellationToken);
    }

    public async Task<bool> VenueExists(UpdateEventCommand model, int venueId, CancellationToken cancellationToken)
    {
        return await _context.Venues
            .AnyAsync(v => v.Id == venueId, cancellationToken);
    }
}
