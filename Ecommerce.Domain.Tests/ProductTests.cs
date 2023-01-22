using System;
using System.Collections.Generic;

using AutoFixture.Xunit2;

using Dawn;

using Ecommerce.Common.Extensions;
using Ecommerce.Common.Tests;

using FluentAssertions;

using Xunit;

namespace Ecommerce.Domain.Tests;

public sealed record ProductTests
{
    private readonly IProductService sut;

    public ProductTests(IProductService sut)
    {
        this.sut = Guard.Argument(sut, nameof(sut)).NotNull().Value;
    }

    [Theory]
    [AutoData]
    public void GivenValidProduct_WhenAddingNew_ThenReturnNewProductId(Product product)
    {
        // Act
        var productId = this.sut.Add(product);

        // Assert
        productId.Should().NotBe(Guid.Empty, "The new id is expected when adding a new product.");
    }
}

internal sealed record ProductTestData
{
    public static IEnumerable<object[]> AddProductTestData()
    {
        return TestData
            .NewSet(new Product("Product1", 4.0))
            .NewSet(new Product("Product2", 4.7));
    }
}

public sealed record Product
{
    public Product(
        string name,
        double price)
    {
        this.Name = name;
        this.Price = price;
    }

    public Guid Id { get; set; } = Guid.Empty;

    public string Name { get; init; }

    public double Price { get; init; }
}

public interface IProductService
{
    Guid Add(Product product);
}

public sealed record ProductService : IProductService
{
    public Guid Add(Product product)
    {
        return Guid.NewGuid();
    }
}
