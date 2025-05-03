using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Seats.Commands.UpdateSeat;

public class UpdateSeatCommandValidator : AbstractValidator<UpdateSeatCommand>
{
    private readonly ITicketingDbContext _context;

    public UpdateSeatCommandValidator(ITicketingDbContext context)
    {
        _context = context;

        RuleFor(r => r.Name)
            .NotEmpty()
            .MaximumLength(20)
            .MustAsync(BeUniqueName);

        RuleFor(r => r.RowId)
            .NotEmpty()
            .MustAsync(RowExists)
            .WithMessage("Row must exist!");

        RuleFor(r => r.PriceId)
            .NotEmpty()
            .MustAsync(PriceExists).
            WithMessage("Price must exist!");
    }

    public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return !await _context.Rows
            .AnyAsync(r => r.Name == name, cancellationToken);
    }

    public async Task<bool> RowExists(int rowId, CancellationToken cancellationToken)
    {
        return !await _context.Rows
            .AnyAsync(s => s.Id == rowId, cancellationToken);
    }

    public async Task<bool> PriceExists(int priceId, CancellationToken cancellationToken)
    {
        return !await _context.Prices
            .AnyAsync(s => s.Id == priceId, cancellationToken);
    }
}
