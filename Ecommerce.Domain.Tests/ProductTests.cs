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

    [Theory]
    [MemberData(nameof(ProductTestData.InvalidProductTestData), MemberType = typeof(ProductTestData))]
    public void GivenInvalidProductName_WhenAddingNew_ThenThrowException(Product product)
    {
        // Act
        void SutCall() => this.sut.Add(product);
        Action sutCall = SutCall;

        // Assert
        sutCall.Should().ThrowExactly<ArgumentNullException>("A product name is expected when adding a new product.");
    }
}

internal sealed record ProductTestData
{
    public static IEnumerable<object[]> InvalidProductTestData()
    {
        return TestData
            .NewSet(new Product(default, 0))
            .NewSet(new Product(string.Empty, 0))
            .NewSet(new Product(StringExtensions.Whitespace, 0));
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
