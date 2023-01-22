using AutoFixture.Xunit2;

using Dawn;

using Ecommerce.Domain.Tests.Product;

using FluentAssertions;

using Xunit;

namespace Ecommerce.Domain.Tests;

public sealed record ProductTests
{
    private readonly IProductServiceFactory sutFactory;

    public ProductTests(IProductServiceFactory sutFactory)
    {
        this.sutFactory = Guard.Argument(sutFactory, nameof(sutFactory)).NotNull().Value;
    }

    [Theory]
    [AutoData]
    public void GivenValidProduct_WhenAddingNew_ThenReturnNewProductId(Domain.Product product)
    {
        // Arrange
        var sut = this.sutFactory.Create(product);
        
        // Act
        var productId = sut.Save();

        // Assert
        productId.Should().NotBe(Guid.Empty, "The new id is expected when adding a new product.");
    }

    [Theory]
    [MemberData(nameof(ProductTestData.InvalidProductTestData), MemberType = typeof(ProductTestData))]
    public void GivenInvalidProductName_WhenAddingNew_ThenThrowException(Domain.Product product)
    {
        // Arrange
        var sut = this.sutFactory.Create(product); 
        
        // Act
        var productId = sut.Save();

        // Assert
        productId.Should().Be(Guid.Empty, "No id should be returned when validation fails.");
    }
}

public interface IProductService
{
    Guid Save();
}

public sealed record ProductService : IProductService
{
    private readonly Domain.Product product;

    public ProductService(Domain.Product product)
    {
        this.product = product;
    }

    public Guid Save()
    {
        var validatedProduct = new ProductValidated(this.product);
        if (!validatedProduct.IsValid)
        {
            return Guid.Empty;
        }
        return Guid.NewGuid();
    }
}

public interface IProductServiceFactory
{
    IProductService Create(Domain.Product product);
}

public sealed record ProductServiceFactory : IProductServiceFactory
{
    public IProductService Create(Domain.Product product) => new ProductService(product);
}
