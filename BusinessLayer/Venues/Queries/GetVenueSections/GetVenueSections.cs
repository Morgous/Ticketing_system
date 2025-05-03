using Ticketing_System.BusinessLayer.Common.Interfaces;
using Ticketing_System.BusinessLayer.Sections.Queries.GetSections;

namespace Ticketing_System.BusinessLayer.Venues.Queries.GetVenueSections;

public record GetVenueSectionsRequest : IRequest<IEnumerable<SectionDto>>
{
    public required int VenueId { get; set; }
}

public class GetVenueSections : IRequestHandler<GetVenueSectionsRequest,IEnumerable<SectionDto>>
{
    private readonly ITicketingDbContext _context;
    private readonly IMapper _mapper;

    public GetVenueSections(ITicketingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SectionDto>> Handle(GetVenueSectionsRequest request, CancellationToken cancellationToken)
    {
        return await _context.Sections
            .AsNoTracking()
            .Where(s => s.VenueId == request.VenueId)
            .ProjectTo<SectionDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
