using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Venues.Queries.GetVenues;

public record GetVenuesRequest : IRequest<IEnumerable<VenueDto>>;

public class GetVenues : IRequestHandler<GetVenuesRequest, IEnumerable<VenueDto>>
{
    private readonly ITicketingDbContext _context;
    private readonly IMapper _mapper;

    public GetVenues(ITicketingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<VenueDto>> Handle(GetVenuesRequest request, CancellationToken cancellationToken)
    {
        return await _context.Venues
            .AsNoTracking()
            .ProjectTo<VenueDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
