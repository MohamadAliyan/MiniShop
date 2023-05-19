
using Application.Contract.Commands;
using Application.Contract.Common.Exceptions;
using Application.Contract.Common.Models;
using EShop.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace EShop.Application.IntegrationTests.Products.Commands;

using static Testing;

public class CreateProductTests : BaseTestFixture
{
   

    [Test]
    public async Task ShouldCreateProduct()
    {

        var command = new CreateProductCommand
        {
        Name = "کالا 1",
        Price = 10000,
        Benefit = 1000,
        Type = Domain.Enums.ProductType.Normal,
    };

        var res= await SendAsync(command);
        res.Should().BePositive();
  
    }
}
