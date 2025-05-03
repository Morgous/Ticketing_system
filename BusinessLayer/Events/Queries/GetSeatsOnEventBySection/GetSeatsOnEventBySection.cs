using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_System.BusinessLayer.Events.Queries.GetSeatsOnEventBySection;

public record GetSeatsOnEventBySectionRequest : IRequest<IEnumerable<EventSeatDto>>
{
    public required int EventId { get; set; }
    public required int SectionId { get; set; }
}

public class GetSeatsOnEventBySectionHandler : IRequestHandler<GetSeatsOnEventBySectionRequest, IEnumerable<EventSeatDto>>
{
    private readonly ITicketingDbContext _context;

    public GetSeatsOnEventBySectionHandler(ITicketingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EventSeatDto>> Handle(GetSeatsOnEventBySectionRequest request, CancellationToken cancellationToken)
    {
        var eventSeats = await _context.EventSeats
            .AsNoTracking()
            .AsSplitQuery()
            .Where(es => es.EventId == request.EventId && es.Seat.Row.SectionId == request.SectionId)
            .Select(es => new EventSeatDto()
            {
                Id = es.Id,
                SectionId = es.Seat.Row.SectionId,
                AmountToPay = es.Seat.Price.AmountToPay,
                SeatId = es.SeatId,
                SeatName = es.Seat.Name,
                State = es.State,
                PriceId = es.Seat.PriceId,
                RowId = es.Seat.RowId
            })
            .ToListAsync(cancellationToken);

        return eventSeats;
    }
}
