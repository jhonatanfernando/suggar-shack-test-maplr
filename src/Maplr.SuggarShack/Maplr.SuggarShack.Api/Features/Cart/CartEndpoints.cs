using Maplr.SuggarShack.Api.Features.Cart.Command;
using Maplr.SuggarShack.Api.Features.Cart.Query;

namespace Maplr.SuggarShack.Api.Features.Cart;

public static class CartEndpoints
{
    public static void MapCartRoutes(this IEndpointRouteBuilder app)
    {
        app.MapGet("/cart", async (IMediator _mediator) =>
        {
            return await _mediator.Send(new GetCartQuery());

        }).WithTags("cart-controller");

        app.MapPut("/cart", async (IMediator _mediator, string productId) =>
        {
            await _mediator.Send(new AddProductCommand(productId));

            return Results.Accepted();

        }).WithTags("cart-controller");

        app.MapDelete("/cart", async (IMediator _mediator,string productId) =>
        {
            await _mediator.Send(new RemoveProductCommand(productId));

            return Results.Accepted();

        }).WithTags("cart-controller");

        app.MapMethods("/cart", new[] { "PATCH" }, async (IMediator _mediator, string productId, int newQty) =>
        {
            await _mediator.Send(new UpdateProductQtyCommand(productId, newQty));

            return Results.Accepted();

        }).WithTags("cart-controller");

    }
}
