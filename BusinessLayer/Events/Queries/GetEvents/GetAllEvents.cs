using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Events.Queries.GetEvents;

public record GetAllEventsRequest : IRequest<IEnumerable<EventDto>>;

public class GetAllEventsHandler : IRequestHandler<GetAllEventsRequest, IEnumerable<EventDto>>
{
    private readonly ITicketingDbContext _context;
    private readonly IMapper _mapper;

    public GetAllEventsHandler(ITicketingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EventDto>> Handle(GetAllEventsRequest request, CancellationToken token)
    {
        return await _context.Events
            .AsNoTracking()
            .ProjectTo<EventDto>(_mapper.ConfigurationProvider)
            .OrderBy(ev => ev.Title)
            .ToListAsync(token);
    }
}
