using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Prices.Queries.GetPrices;

public record GetPricesRequest : IRequest<IEnumerable<PriceDto>>;

public class GetPricesRequestHandler : IRequestHandler<GetPricesRequest, IEnumerable<PriceDto>>
{
    private readonly ITicketingDbContext _context;
    private readonly IMapper _mapper;

    public GetPricesRequestHandler(ITicketingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PriceDto>> Handle(GetPricesRequest request, CancellationToken token)
    {
        return await _context.Prices
            .AsNoTracking()
            .ProjectTo<PriceDto>(_mapper.ConfigurationProvider)
            .ToListAsync(token);
    }
}
