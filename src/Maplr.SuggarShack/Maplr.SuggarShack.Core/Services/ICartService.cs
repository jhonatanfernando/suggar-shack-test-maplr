namespace Maplr.SuggarShack.Core.Services;

public interface ICartService
{
    Task AddProductAsync(string ProductId, CancellationToken token = default);
    Task<List<CartLineDto>> GetAsync(CancellationToken token = default);
    Task RemoveProductAsync(string productId, CancellationToken token = default);
    Task UpdateProductQuantityAsync(string productId, int newQty, CancellationToken token = default);
}
