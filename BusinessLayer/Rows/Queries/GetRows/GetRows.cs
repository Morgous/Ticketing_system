using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Rows.Queries.GetRows;

public record GetRowsRequest : IRequest<IEnumerable<RowDto>>;

public class GetRowsRequestHandler : IRequestHandler<GetRowsRequest, IEnumerable<RowDto>>
{
    private readonly ITicketingDbContext _context;
    private readonly IMapper _mapper;

    public GetRowsRequestHandler(ITicketingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RowDto>> Handle(GetRowsRequest request, CancellationToken cancellationToken)
    {
        return await _context.Rows
            .AsNoTracking()
            .ProjectTo<RowDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
