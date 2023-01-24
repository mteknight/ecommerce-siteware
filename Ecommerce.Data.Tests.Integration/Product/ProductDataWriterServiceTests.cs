using Dawn;

using Ecommerce.Common.Data;
using Ecommerce.Domain;

using FluentAssertions;

using Xunit;

namespace Ecommerce.Data.Tests.Integration;

public sealed record ProductDataWriterServiceTests
{
    private readonly IAggregateDataWriterService<Product, ProductValidated> sut;

    public ProductDataWriterServiceTests(IAggregateDataWriterService<Product, ProductValidated> sut)
    {
        this.sut = Guard.Argument(sut, nameof(sut)).NotNull().Value;
    }

    [Theory]
    [MemberData(nameof(ProductTestData.ValidProductTestData), MemberType = typeof(ProductTestData))]
    public void GivenValidProduct_WhenAddingNew_ThenReturnAddedProduct(Product product)
    {
        // Arrange
        var productValidated = new ProductValidated(product);

        // Act
        var wasAdded = this.sut.Add(productValidated);
        var newProduct = this.sut.Get(product.Id);

        // Assert
        wasAdded.Should().BeTrue("The new product is expected to be added successfully.");
        newProduct.Should().NotBeNull("The new product was added successfully and should be retrievable.");
        newProduct.Should().Be(product, "Same product is expected from the repository.");
    }
}
