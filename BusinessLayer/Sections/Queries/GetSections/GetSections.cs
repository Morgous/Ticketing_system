using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Sections.Queries.GetSections;

public record GetSectionsRequest : IRequest<IEnumerable<SectionDto>>;

public class GetSections : IRequestHandler<GetSectionsRequest, IEnumerable<SectionDto>>
{
    private readonly ITicketingDbContext _context;
    private readonly IMapper _mapper;

    public GetSections(ITicketingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SectionDto>> Handle(GetSectionsRequest request, CancellationToken cancellationToken)
    {
        return await _context.Sections
            .AsNoTracking()
            .ProjectTo<SectionDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
