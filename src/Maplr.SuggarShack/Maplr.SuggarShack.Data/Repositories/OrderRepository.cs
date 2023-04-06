namespace Maplr.SuggarShack.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly MaplrContext _maplrContext;

    public OrderRepository(MaplrContext maplrContext)
    {
        _maplrContext = maplrContext;
    }

    public async Task<int> CreateAsync(Order order, CancellationToken token = default)
    {
        _maplrContext.Orders.Add(order);

        return await _maplrContext.SaveChangesAsync(token);
    }
}
