using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Ticketing_system.DataAccessLayer;
using Ticketing_system.Models.Payments;
using Ticketing_System.BusinessLayer.Payments.Commands.CompletePayment;
using Ticketing_System.BusinessLayer.Payments.Commands.FailPayment;
using Ticketing_System.BusinessLayer.Payments.Queries.GetPayment;

namespace Ticketing_systemEndpoints;

public class Payments : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetPayment, "{paymentId}")
            .MapPost(FailPayment, "{paymentId}/failed")
            .MapPost(CompletePayment, "{paymentId}/completed");
    }

    public async Task<Ok<PaymentStateResponse>> GetPayment(ISender sender, IMapper mapper, int paymentId)
    {
        var paymentState = await sender.Send(new GetPaymentRequest(paymentId));

        var result = mapper.Map<PaymentStateResponse>(paymentState);

        return TypedResults.Ok(result);
    }

    public async Task<NoContent> FailPayment(ISender sender, int paymentId)
    {
        await sender.Send(new FailPaymentCommand(paymentId));

        return TypedResults.NoContent();
    }

    public async Task<NoContent> CompletePayment(ISender sender, int paymentId)
    {
        await sender.Send(new CompletePaymentCommand(paymentId));

        return TypedResults.NoContent();
    }
}
