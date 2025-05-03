using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Prices.Commands.UpdatePrice;

internal class UpdatePriceCommandValidator : AbstractValidator<UpdatePriceCommand>
{
    private readonly ITicketingDbContext _context;

    public UpdatePriceCommandValidator(ITicketingDbContext context)
    {
        _context = context;

        RuleFor(v => v.AmountToPay)
            .NotEmpty()
            .Must(p => p > 0)
            .WithMessage("Price must be above zero!");
    }
}
