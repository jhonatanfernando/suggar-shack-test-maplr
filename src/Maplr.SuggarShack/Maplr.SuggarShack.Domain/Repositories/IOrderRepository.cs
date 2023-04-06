using Maplr.SuggarShack.Domain.Entities;

namespace Maplr.SuggarShack.Domain.Repositories;

public interface IOrderRepository
{
    Task<int> CreateAsync(Order order, CancellationToken token = default);
}
