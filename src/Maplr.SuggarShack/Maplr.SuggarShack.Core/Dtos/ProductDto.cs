namespace Maplr.SuggarShack.Core.Dtos;

public class ProductDto
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string Image { get; init; }
    public double Price { get; init; }
    public ProductType Type { get; init; }
}
