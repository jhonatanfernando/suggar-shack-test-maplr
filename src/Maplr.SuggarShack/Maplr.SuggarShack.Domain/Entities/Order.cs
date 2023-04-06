namespace Maplr.SuggarShack.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public List<OrderItems> Items { get; set; }
}
