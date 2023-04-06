namespace Maplr.SuggarShack.Api.Features.Products.Query;

public class GetProductsByTypeQuery : IRequest<IEnumerable<CatalogueItemDto>>
{
    private readonly ProductType _productType;

    public GetProductsByTypeQuery(ProductType productType)
    {
        _productType = productType;
    }

    public class GetProductsByTypeQueryHandler : IRequestHandler<GetProductsByTypeQuery, IEnumerable<CatalogueItemDto>>
    {
        private readonly IProductService _productService;

        public GetProductsByTypeQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IEnumerable<CatalogueItemDto>> Handle(GetProductsByTypeQuery query, CancellationToken cancellationToken)
        {
            return await _productService.GetByTypeAsync(query._productType, cancellationToken);
        }
    }
}