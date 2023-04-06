using Maplr.SuggarShack.Api.Features.Products.Query;
using Maplr.SuggarShack.Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace Maplr.SuggarShack.Api.Features.Products;

public static class ProductEndpoints
{
    public static void MapProductRoutes(this IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (IMediator _mediator, [FromQuery] ProductType type) =>
        {
            return await _mediator.Send(new GetProductsByTypeQuery(type));

        }).WithTags("products-controller");

        app.MapGet("/products/{productId}", async (IMediator _mediator, Guid productId) =>
        {
            return await _mediator.Send(new GetProductsByIdQuery(productId));

        }).WithTags("products-controller");

    }
}
