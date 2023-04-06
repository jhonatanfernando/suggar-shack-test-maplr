namespace Maplr.SuggarShack.Core.Extensions;

public static class ProductExtensions
{
    public static Dtos.CatalogueItemDto ToCatalogueItemDto(this Product product)
    {
        return new()
        {
            Id = product.Id.ToString(),
            Image = product.Image,
            Name = product.Name,
            Price = product.Price,
            MaxQty = product.Stock,
            Type = product.Type,
        };
    }

    public static IEnumerable<Dtos.CatalogueItemDto> ToCatalogueItemDto(this IEnumerable<Product> products)
    {
        return products.Select(m => m.ToCatalogueItemDto());
    }
    public static MapleSyrupDto ToMapleSyrupDto(this Product product)
    {
        return new()
        {
            Id = product.Id.ToString(),
            Image = product.Image,
            Name = product.Name,
            Description = product.Name,
            Price = product.Price,
            Stock = product.Stock,
            Type = product.Type,
        };
    }

    public static IEnumerable<MapleSyrupDto> ToDto(this IEnumerable<Product> products)
    {
        return products.Select(m => m.ToMapleSyrupDto());
    }

    public static CartLineDto ToCartLineDto(this Product product)
    {
        return new()
        {
            ProductId = product.Id.ToString(),
            Image = product.Image,
            Name = product.Name,
            Price = product.Price,
        };
    }
}
