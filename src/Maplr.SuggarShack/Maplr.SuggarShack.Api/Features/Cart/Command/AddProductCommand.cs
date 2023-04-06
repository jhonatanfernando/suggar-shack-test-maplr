namespace Maplr.SuggarShack.Api.Features.Cart.Command;

public class AddProductCommand : IRequest<Unit>
{
    private readonly string _productId;

    public AddProductCommand(string productId)
    {
        _productId = productId;
    }

    public class AddProductToCartCommandHandler : IRequestHandler<AddProductCommand, Unit>
    {
        private readonly ICartService _cartService;

        public AddProductToCartCommandHandler(ICartService cartService)
        {
           _cartService = cartService;
        }

        public async Task<Unit> Handle(AddProductCommand command, CancellationToken cancellationToken)
        {
            await _cartService.AddProductAsync(command._productId, cancellationToken);

            return Unit.Value;
        }
    }
}
