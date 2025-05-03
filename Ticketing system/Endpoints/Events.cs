using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Ticketing_system.DataAccessLayer;
using Ticketing_system.Models.EventSeat;
using Ticketing_System.BusinessLayer.Events.Queries.GetEvents;
using Ticketing_System.BusinessLayer.Events.Queries.GetSeatsOnEventBySection;

namespace Ticketing_system.Endpoints;

public class Events : EndpointGroupBase
{
    public override void Map(WebApplication app) 
    {
        app.MapGroup(this)
            .MapGet(GetEvents)
            .MapGet(GetSeatsForEventBySector, "{eventId}/sections/{sectionId}/seats");
    }

    public async Task<Ok<IEnumerable<EventDto>>> GetEvents(ISender sender)
    {
        var venues = await sender.Send(new GetAllEventsRequest());

        return TypedResults.Ok(venues);
    }

    public async Task<Ok<IEnumerable<EventSeatResponse>>> GetSeatsForEventBySector(ISender sender , IMapper mapper, int eventId, int sectionId)
    {
        var seats = await sender.Send(new GetSeatsOnEventBySectionRequest() { EventId = eventId, SectionId = sectionId });

        var result = mapper.Map<IEnumerable<EventSeatResponse>>(seats);

        return TypedResults.Ok(result);
    }
}
