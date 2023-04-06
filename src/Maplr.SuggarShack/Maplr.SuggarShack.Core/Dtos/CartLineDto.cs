namespace Maplr.SuggarShack.Core.Dtos;

public class CartLineDto
{
    public string ProductId { get; init; }
    public string Name { get; init; }
    public string Image { get; init; }
    public double Price { get; init; }
    public int Qty { get; set; }
}
