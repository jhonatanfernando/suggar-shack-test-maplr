using FluentAssertions;
using Maplr.SuggarShack.Core.Dtos;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Maplr.SuggarShack.Tests.Services;

public class CartServiceShould : UnitTestBase<CartService>
{
    [Fact]
    public async Task AddProduct_WhenAddProductAsyncIsCalled()
    {
        //Arrange
        var cart = new List<CartLineDto>();
        var productId = Guid.NewGuid();

        Mocker.GetMock<IProductRepository>().Setup(c => c.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Product()
        {
            Description = "description",
            Id = productId,
            Name = "name",
            Image = "image",
            Price = 0,
            Stock = 0,
            Type = ProductType.CLEAR

        });

        Mocker.GetMock<IHttpContextAccessor>().Setup(c => c.HttpContext.Session.Set(It.IsAny<string>(), It.IsAny<byte[]>())).Callback((string key, byte[] value) =>
        {
            cart = JsonSerializer.Deserialize<List<CartLineDto>>(Encoding.UTF8.GetString(value));
        });

        //Act
        await Sut.AddProductAsync(productId.ToString());

        //Assert
        cart.Should().NotBeNull();
        cart.Should().HaveCountGreaterThanOrEqualTo(1);
        cart.First().ProductId.Should().Be(productId.ToString());
    }

    [Fact]
    public async Task ReturnCart_WhenGetAsyncIsCalled()
    {
        //Arrange
        var cart = new List<CartLineDto>()
        {
            new CartLineDto()
            {
                 Image = "image",
                 Name= "name",
                 Price = 1,
                 ProductId= Guid.NewGuid().ToString(),
                 Qty = 1
            }
        };

        var data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(cart));
        Mocker.GetMock<IHttpContextAccessor>().Setup(c => c.HttpContext.Session.TryGetValue(It.IsAny<string>(), out data)).Returns(true);


        //Act
        var cartResult = await Sut.GetAsync();

        //Assert
        cartResult.Should().NotBeNull();
        cartResult.Should().HaveCountGreaterThanOrEqualTo(1);
    }

    [Fact]
    public async Task RemoveProduct_WhenRemoveProductAsyncIsCalled()
    {
        //Arrange
        var productId = Guid.NewGuid();

        var cart = new List<CartLineDto>()
        {
            new CartLineDto()
            {
                 Image = "image",
                 Name= "name",
                 Price = 1,
                 ProductId= Guid.NewGuid().ToString(),
                 Qty = 1
            },

            new CartLineDto()
            {
                 Image = "image",
                 Name= "name",
                 Price = 1,
                 ProductId= productId.ToString(),
                 Qty = 1
            }
        };

        var data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(cart));

        Mocker.GetMock<IHttpContextAccessor>().Setup(c => c.HttpContext.Session.TryGetValue(It.IsAny<string>(), out data)).Returns(true);

        Mocker.GetMock<IHttpContextAccessor>().Setup(c => c.HttpContext.Session.Set(It.IsAny<string>(), It.IsAny<byte[]>())).Callback((string key, byte[] value) =>
        {
            cart = JsonSerializer.Deserialize<List<CartLineDto>>(Encoding.UTF8.GetString(value));
        });


        //Act
        await Sut.RemoveProductAsync(productId.ToString());

        //Assert
        cart.Should().NotBeNull();
        cart.Should().NotContain(c=> c.ProductId == productId.ToString());  
    }

    [Fact]
    public async Task UpdateProductQty_WhenUpdateProductQuantityAsyncIsCalled()
    {
        //Arrange
        var productId = Guid.NewGuid();
        var newQty = 20;

        var cart = new List<CartLineDto>()
        {
            new CartLineDto()
            {
                 Image = "image",
                 Name= "name",
                 Price = 1,
                 ProductId= productId.ToString(),
                 Qty = 1
            }
        };

        var data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(cart));

        Mocker.GetMock<IHttpContextAccessor>().Setup(c => c.HttpContext.Session.TryGetValue(It.IsAny<string>(), out data)).Returns(true);

        Mocker.GetMock<IHttpContextAccessor>().Setup(c => c.HttpContext.Session.Set(It.IsAny<string>(), It.IsAny<byte[]>())).Callback((string key, byte[] value) =>
        {
            cart = JsonSerializer.Deserialize<List<CartLineDto>>(Encoding.UTF8.GetString(value));
        });


        //Act
        await Sut.UpdateProductQuantityAsync(productId.ToString(), newQty);

        //Assert
        cart.Should().NotBeNull();
        cart.First().Qty.Should().Be(newQty);
    }
}
