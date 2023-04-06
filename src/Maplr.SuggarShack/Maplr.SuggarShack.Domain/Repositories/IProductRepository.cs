using Maplr.SuggarShack.Domain.Entities;
using Maplr.SuggarShack.Domain.Enum;

namespace Maplr.SuggarShack.Domain.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetByTypeAsync(ProductType type, CancellationToken token = default);
    Task<Product> GetByIdAsync(Guid id, CancellationToken token = default);
}
