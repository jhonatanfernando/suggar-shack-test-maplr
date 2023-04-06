using Maplr.SuggarShack.Api.Features.Order.Command;

namespace Maplr.SuggarShack.Api.Features.Order;

public static class OrderEndpoints
{
    public static void MapOrderRoutes(this IEndpointRouteBuilder app)
    {
        app.MapPost("/order", async (IMediator _mediator, OrderLineDto[] items) =>
        {
            return await _mediator.Send(new CreateOrderCommand(items));

        }).WithTags("order-controller");

    }
}
