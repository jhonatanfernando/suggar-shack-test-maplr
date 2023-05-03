using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Maplr.SuggarShack.Tests.Services;

public class ProductServiceShould : UnitTestBase<ProductService>
{
    [Fact]
    public async Task ReturnCatalogueItem_WhenCallGetByTypeAsync()
    {
        //Arrange
        var id = Guid.NewGuid();

        Mocker.GetMock<IProductRepository>().Setup(c => c.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Product()
        {
            Description = "description",
            Id = id,
            Name = "name",
            Image = "image",
            Price = 0,
            Stock = 0,
            Type = ProductType.CLEAR

        });

        //Act
        var mapleSyrupItem = await Sut.GetByIdAsync(id);

        //Assert
        mapleSyrupItem.Should().NotBeNull();
        mapleSyrupItem.Id.Should().Be(id.ToString());
    }

    [Theory]
    [InlineData(ProductType.AMBER)]
    [InlineData(ProductType.CLEAR)]
    [InlineData(ProductType.DARK)]
    public async Task ReturnProductsByType_WhenCallGetByTypeAsync(ProductType productType)
    {
        //Arrange
        Mocker.GetMock<IProductRepository>().Setup(c => c.GetByTypeAsync(It.IsAny<ProductType>(), It.IsAny<CancellationToken>())).ReturnsAsync(new List<Product>()
        {
            new Product()
            {
                Description = "description",
                Id = Guid.NewGuid(),
                Name = "name",
                Image = "image",
                Price = 0,
                Stock = 0,
                Type = productType
            }
        });

        //Act
        var catalogueItems = await Sut.GetByTypeAsync(productType);

        //Assert
        catalogueItems.Should().NotContainNulls();
        catalogueItems.Should().OnlyContain(c => c.Type == productType);
    }
}
