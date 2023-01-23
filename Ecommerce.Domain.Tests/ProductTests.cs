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
    public void GivenInvalidProductName_WhenAddingNew_ThenSetInvalidAndReturnEmptyId(Domain.Product product)
    {
        // Arrange
        var sut = this.sutFactory.Create(product); 
        
        // Act
        var productId = sut.Save();

        // Assert
        sut.ValidatedAggregate.IsValid.Should().BeFalse("Expected to be false when validation fails.");
        productId.Should().Be(Guid.Empty, "No id should be returned when validation fails.");
    }
    
    [Theory]
    [MemberData(nameof(ProductTestData.ValidProductTestData), MemberType = typeof(ProductTestData))]
    public void GivenProductDiscount_WhenAddingNew_ThenReturnNewProductId(Domain.Product product)
    {
        // Arrange
        var sut = this.sutFactory.Create(product); 
        
        // Act
        var productId = sut.Save();

        // Assert
        product.Promotion.Should().NotBeNull("At least the default promotion (NoPromotion) is expected.");
        productId.Should().NotBeEmpty("The new id is expected when adding a new product.");
    }
}
