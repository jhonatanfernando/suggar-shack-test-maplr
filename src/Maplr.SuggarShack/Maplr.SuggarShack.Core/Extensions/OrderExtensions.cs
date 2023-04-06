namespace Maplr.SuggarShack.Core.Extensions;

public static class OrderExtensions
{
    public static OrderItems ToModel(this OrderLineDto orderLineDto)
    {
        return new()
        {
            ProductId = Guid.Parse(orderLineDto.ProductId),
            Quantity = orderLineDto.Qty
        };
    }

    public static IEnumerable<OrderItems> ToModel(this IEnumerable<OrderLineDto> orderLineDtos)
    {
        return orderLineDtos.Select(m => m.ToModel());
    }
}
