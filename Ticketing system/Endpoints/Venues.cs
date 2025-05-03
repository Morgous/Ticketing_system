using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Ticketing_system.DataAccessLayer;
using Ticketing_system.Models.Sections;
using Ticketing_system.Models.Venues;
using Ticketing_System.BusinessLayer.Venues.Queries.GetVenues;
using Ticketing_System.BusinessLayer.Venues.Queries.GetVenueSections;

namespace Ticketing_systemEndpoints;

public class Venues : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetVenues)
            .MapGet(GetVenueSections, "{venueId}/sections");
    }

    public async Task<Ok<IEnumerable<VenuesResponse>>> GetVenues(ISender sender, IMapper mapper)
    {
        var venues = await sender.Send(new GetVenuesRequest());

        var result = mapper.Map<IEnumerable<VenuesResponse>>(venues);

        return TypedResults.Ok(result);
    }

    public async Task<Ok<IEnumerable<SectionResponse>>> GetVenueSections(ISender sender, IMapper mapper, int venueId)
    {
        var venues = await sender.Send(new GetVenueSectionsRequest() { VenueId = venueId} );

        var result = mapper.Map<IEnumerable<SectionResponse>>(venues);

        return TypedResults.Ok(result);
    }
}
