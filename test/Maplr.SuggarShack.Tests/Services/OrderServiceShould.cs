using Moq;
using System.Threading;
using System;
using System.Threading.Tasks;
using Xunit;
using Maplr.SuggarShack.Core.Dtos;
using FluentAssertions;

namespace Maplr.SuggarShack.Tests.Services;

public class OrderServiceShould : UnitTestBase<OrderService>
{
    [Fact]
    public async Task ReturnErros_WhenProductDoesNotExist()
    {
        //Arrange
        Mocker.GetMock<IProductRepository>().Setup(c => c.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Product()
        {
            Description = "description",
            Id = Guid.NewGuid(),
            Name = "name",
            Image = "image",
            Price = 0,
            Stock = 0,
            Type = ProductType.CLEAR

        });

        //Act
        var orderValidation = await Sut.PlaceOrderAsync(new OrderLineDto[] { 
            new OrderLineDto()
            {
                ProductId = Guid.NewGuid().ToString(),
                Qty = 1
            } 
        });

        //Assert
        orderValidation.Errors.Should().NotBeNull();
        orderValidation.Errors.Should().HaveCountGreaterThanOrEqualTo(1);
    }

    [Fact]
    public async Task ReturnErros_WhenProductQtyIsGreatherThanStockValue()
    {
        //Arrange
        var producId = Guid.NewGuid();

        Mocker.GetMock<IProductRepository>().Setup(c => c.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Product()
        {
            Description = "description",
            Id = producId,
            Name = "name",
            Image = "image",
            Price = 0,
            Stock = 10,
            Type = ProductType.CLEAR

        });

        //Act
        var orderValidation = await Sut.PlaceOrderAsync(new OrderLineDto[] {
            new OrderLineDto()
            {
                ProductId = producId.ToString(),
                Qty = 11
            }
        });

        //Assert
        orderValidation.Errors.Should().NotBeNull();
        orderValidation.Errors.Should().HaveCountGreaterThanOrEqualTo(1);
    }

    [Fact]
    public async Task ReturnNoErros_WhenProductAndQtyAreValid()
    {
        //Arrange
        var producId = Guid.NewGuid();

        Mocker.GetMock<IProductRepository>().Setup(c => c.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Product()
        {
            Description = "description",
            Id = producId,
            Name = "name",
            Image = "image",
            Price = 0,
            Stock = 10,
            Type = ProductType.CLEAR

        });

        //Act
        var orderValidation = await Sut.PlaceOrderAsync(new OrderLineDto[] {
            new OrderLineDto()
            {
                ProductId = producId.ToString(),
                Qty = 9
            }
        });

        //Assert
        orderValidation.Errors.Should().NotBeNull();
        orderValidation.Errors.Should().BeEmpty();
    }
}
