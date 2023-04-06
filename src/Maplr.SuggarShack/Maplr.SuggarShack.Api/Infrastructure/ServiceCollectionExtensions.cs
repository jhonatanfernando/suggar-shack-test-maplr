using Maplr.SuggarShack.Data.Repositories;
using Maplr.SuggarShack.Domain.Repositories;
using Maplr.SuggarShack.Service.Services;

namespace Maplr.SuggarShack.Api.Infrastructure;

public static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IProductService, ProductService>()
            .AddScoped<IOrderService, OrderService>()
            .AddScoped<ICartService, CartService>();
    }

    internal static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<IOrderRepository, OrderRepository>();
    }

}
