using Maplr.SuggarShack.Domain.Enum;

namespace Maplr.SuggarShack.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }
    public ProductType Type { get; set; }
}
