namespace Maplr.SuggarShack.Api.Features.Cart.Command;

public class UpdateProductQtyCommand : IRequest<Unit>
{
    private readonly string _productId;
    private readonly int _newQty;

    public UpdateProductQtyCommand(string productId, int newQty)
    {
        _productId = productId;
        _newQty = newQty;
    }

    public class UpdateProductQtyCommandHandler : IRequestHandler<UpdateProductQtyCommand, Unit>
    {
        private readonly ICartService _cartService;

        public UpdateProductQtyCommandHandler(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<Unit> Handle(UpdateProductQtyCommand command, CancellationToken cancellationToken)
        {
            await _cartService.UpdateProductQuantityAsync(command._productId, command._newQty, cancellationToken);

            return Unit.Value;
        }
    }
}