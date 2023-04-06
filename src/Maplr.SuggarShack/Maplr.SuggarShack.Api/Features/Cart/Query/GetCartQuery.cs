namespace Maplr.SuggarShack.Api.Features.Cart.Query;

public class GetCartQuery : IRequest<List<CartLineDto>>
{
    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, List<CartLineDto>>
    {
        private readonly ICartService _cartService;

        public GetCartQueryHandler(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<List<CartLineDto>> Handle(GetCartQuery query, CancellationToken cancellationToken)
        {
            return await _cartService.GetAsync(cancellationToken);
        }
    }
}
