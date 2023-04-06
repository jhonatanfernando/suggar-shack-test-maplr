using Maplr.SuggarShack.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Maplr.SuggarShack.Service.Services;

public class CartService : ICartService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IProductRepository _productRepository;
    private readonly ILogger _logger;

    public CartService(IHttpContextAccessor httpContextAccessor,
                       IProductRepository productRepository,
                       ILogger<CartService> logger)
    {
       _httpContextAccessor = httpContextAccessor;
       _productRepository = productRepository;
       _logger = logger;
    }

    public async Task<List<CartLineDto>> GetAsync(CancellationToken token = default)
    {
        return await GetDataFromSession(token);
    }

    public async Task AddProductAsync(string productId, CancellationToken token = default)
    {
        var items = await GetDataFromSession(token);

        Guid.TryParse(productId, out var id);
        var product = await _productRepository.GetByIdAsync(id);

        if (product != null)
        {
            items.Add(product.ToCartLineDto());

            _httpContextAccessor.HttpContext.Session.SetString(Constants.CartSession, JsonSerializer.Serialize(items));
        }
    }

    public async Task RemoveProductAsync(string productId, CancellationToken token = default)
    {
        var items = await GetDataFromSession(token);

        var item = items.SingleOrDefault(c => c.ProductId == productId);
     
        if (item != null)
        {
            items.Remove(item);
        }

        _httpContextAccessor.HttpContext.Session.SetString(Constants.CartSession, JsonSerializer.Serialize(items));
    }

    public async Task UpdateProductQuantityAsync(string productId, int newQty, CancellationToken token = default)
    {
        var items = await GetDataFromSession(token);

        var item = items.SingleOrDefault(c => c.ProductId == productId);

        if (item != null)
        {
            item.Qty = newQty; 
        }

        _httpContextAccessor.HttpContext.Session.SetString(Constants.CartSession, JsonSerializer.Serialize(items));
    }

    private async Task<List<CartLineDto>> GetDataFromSession(CancellationToken token)
    {
        await _httpContextAccessor.HttpContext.Session.LoadAsync(token);

        var sessionData = _httpContextAccessor.HttpContext.Session.GetString(Constants.CartSession);

        var items = new List<CartLineDto>();

        try
        {
            if (sessionData != null)
            {
                items = JsonSerializer.Deserialize<List<CartLineDto>>(sessionData);
            }
        }
        catch
        {
            _logger.LogInformation("It failed to get data from the session.");
        }

        return items;
    }
}
