namespace Maplr.SuggarShack.Api.Features.Cart.Command;

public class RemoveProductCommand : IRequest<Unit>
{
    private readonly string _productId;

    public RemoveProductCommand(string productId)
    {
        _productId = productId;
    }

    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand, Unit>
    {
        private readonly ICartService _cartService;

        public RemoveProductCommandHandler(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<Unit> Handle(RemoveProductCommand command, CancellationToken cancellationToken)
        {
            await _cartService.RemoveProductAsync(command._productId, cancellationToken);

            return Unit.Value;
        }
    }
}
