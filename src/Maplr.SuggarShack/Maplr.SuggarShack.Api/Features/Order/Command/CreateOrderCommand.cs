namespace Maplr.SuggarShack.Api.Features.Order.Command;

public class CreateOrderCommand : IRequest<OrderValidationResponseDto>
{
    private readonly OrderLineDto[] _items;

    public CreateOrderCommand(OrderLineDto[] items)
	{
		_items = items;
    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderValidationResponseDto>
    {
        private readonly IOrderService _orderService;

        public CreateOrderCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<OrderValidationResponseDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderValidation = await _orderService.PlaceOrderAsync(request._items, cancellationToken);

            return orderValidation;
        }
    }
}
