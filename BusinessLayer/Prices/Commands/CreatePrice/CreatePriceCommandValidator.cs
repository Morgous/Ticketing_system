using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Prices.Commands.CreatePrice;
internal class CreatePriceCommandValidator : AbstractValidator<CreatePriceCommand>
{
    private readonly ITicketingDbContext _context;

    public CreatePriceCommandValidator(ITicketingDbContext context)
    {
        _context = context;

        RuleFor(v => v.AmountToPay)
            .NotEmpty()
            .Must(p => p > 0)
            .WithMessage("Price must be above zero!");
    }
}
