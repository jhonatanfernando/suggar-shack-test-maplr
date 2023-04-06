using Maplr.SuggarShack.Domain.Entities;

namespace Maplr.SuggarShack.Service.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public OrderService(IOrderRepository orderRepository,
                        IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }
    public async Task<OrderValidationResponseDto> PlaceOrderAsync(OrderLineDto[] items, CancellationToken token = default)
    {
        var errors = await ValidateItems(items, token);

        if (!errors.Any())
        {
            var order = new Order
            {
                Items = items.ToModel().ToList()
            };

            await _orderRepository.CreateAsync(order, token);
        }

        var orderValidationResponseDto = new OrderValidationResponseDto()
        {
            Errors = errors.ToArray(),
            IsOrderValid = !errors.Any()
        };

        return orderValidationResponseDto;
    }

    private async Task<List<string>> ValidateItems(OrderLineDto[] items, CancellationToken token)
    {
        List<string> errors = new();

        foreach (var item in items)
        {
            _ = Guid.TryParse(item.ProductId, out var producId);

            var product = await _productRepository.GetByIdAsync(producId, token);
            if (product == null)
            {
                errors.Add($"The Product with Id {item.ProductId} does not exist");
            }
            else if (item.Qty > product.Stock)
            {
                errors.Add($"The Qty {item.Qty} is greather than the quantity avaiable in stock {product.Stock} for the Product {product.Name}");
            }
        }

        return errors;
    }
}
