namespace Maplr.SuggarShack.Core.Services;

public interface IOrderService
{
    Task<OrderValidationResponseDto> PlaceOrderAsync(OrderLineDto[] items, CancellationToken token = default);
}
