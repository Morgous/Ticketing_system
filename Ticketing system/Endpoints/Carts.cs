using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Ticketing_system.DataAccessLayer;
using Ticketing_system.Models.Cart;
using Ticketing_System.BusinessLayer.Carts.Commands.CreateSeatInCart;
using Ticketing_System.BusinessLayer.Carts.Commands.DeleteSeatFromCart;
using Ticketing_System.BusinessLayer.Carts.Commands.UpdateSeatsInCart;
using Ticketing_System.BusinessLayer.Carts.Queries.GetCart;

namespace Ticketing_system.Endpoints;

public class Carts : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetCart, "{cartId}")
            .MapPost(CreateSeatInCart)
            .MapDelete(DeleteSeatFromCart, "{cartId}/eventSeats/{eventSeatId}")
            .MapPut(BookSeatInCart, "{cartId}/book");
    }

    public async Task<Ok<CartResponse>> GetCart(ISender sender, IMapper mapper, Guid cartId)
    {
        var cart = await sender.Send(new GetCartRequest() { CartId = cartId });

        var result = mapper.Map<CartResponse>(cart);

        return TypedResults.Ok(result);
    }

    public async Task<Ok<CartResponse>> CreateSeatInCart(ISender sender, IMapper mapper, CreateSeatInCartCommand command)
    {
        await sender.Send(command);

        var cart = await sender.Send(new GetCartRequest() { CartId = command.CartId });

        var result = mapper.Map<CartResponse>(cart);

        return TypedResults.Ok(result);
    }

    public async Task<NoContent> DeleteSeatFromCart(ISender sender, Guid cartId, int eventSeatId)
    {
        await sender.Send(new DeleteSeatFromCartCommant()
        {
            CartId = cartId,
            EventSeatId = eventSeatId
        });

        return TypedResults.NoContent();
    }

    public async Task<Ok<int>> BookSeatInCart(ISender sender, Guid cartId)
    {
        var paymentId = await sender.Send(new BookSeatsInCartCommand(cartId));

        return TypedResults.Ok(paymentId);
    }
}
