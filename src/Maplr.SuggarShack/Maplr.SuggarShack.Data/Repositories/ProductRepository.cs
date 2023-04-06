using Maplr.SuggarShack.Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace Maplr.SuggarShack.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly MaplrContext _maplrContext;

    public ProductRepository(MaplrContext maplrContext)
    {
        _maplrContext = maplrContext;
    }

    public async Task<IEnumerable<Product>> GetByTypeAsync(ProductType type, CancellationToken token = default)
    {
        return await _maplrContext.Products
            .Where(p => p.Type == type)
            .ToListAsync(token);
    }

    public async Task<Product> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await _maplrContext.Products.Where(m => m.Id == id).SingleOrDefaultAsync(token);
    }
}
