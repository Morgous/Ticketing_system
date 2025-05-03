using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Seats.Queries.GetSeats;

public record GetSeatsRequest : IRequest<IEnumerable<SeatDto>>;

public class GetSeatsRequestHandler : IRequestHandler<GetSeatsRequest, IEnumerable<SeatDto>>
{
    private readonly ITicketingDbContext _context;
    private readonly IMapper _mapper;

    public GetSeatsRequestHandler(ITicketingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SeatDto>> Handle(GetSeatsRequest request, CancellationToken cancellationToken)
    {
        return await _context.Seats
            .AsNoTracking()
            .ProjectTo<SeatDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
