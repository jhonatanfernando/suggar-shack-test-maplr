namespace Maplr.SuggarShack.Core.Services;

public interface IProductService
{
    Task<IEnumerable<CatalogueItemDto>> GetByTypeAsync(ProductType type, CancellationToken token = default);
    Task<MapleSyrupDto> GetByIdAsync(Guid id, CancellationToken token = default);
}
