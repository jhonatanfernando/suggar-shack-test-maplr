using Maplr.SuggarShack.Domain.Enum;

namespace Maplr.SuggarShack.Service.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<CatalogueItemDto>> GetByTypeAsync(ProductType type, CancellationToken token = default)
    {
        return (await _productRepository.GetByTypeAsync(type,token)).ToCatalogueItemDto();
    }

    public async Task<MapleSyrupDto> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return (await _productRepository.GetByIdAsync(id, token)).ToMapleSyrupDto();
    }
}
