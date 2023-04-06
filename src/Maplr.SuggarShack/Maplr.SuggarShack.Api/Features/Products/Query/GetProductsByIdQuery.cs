namespace Maplr.SuggarShack.Api.Features.Products.Query;

public class GetProductsByIdQuery : IRequest<MapleSyrupDto>
{
    private readonly Guid _productId;

    public GetProductsByIdQuery(Guid productId)
    {
        _productId = productId;
    }

    public class GetProductsByIdQueryHandler : IRequestHandler<GetProductsByIdQuery, MapleSyrupDto>
    {
        private readonly IProductService _productService;

        public GetProductsByIdQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<MapleSyrupDto> Handle(GetProductsByIdQuery query, CancellationToken cancellationToken)
        {
            return await _productService.GetByIdAsync(query._productId, cancellationToken);
        }
    }
}
