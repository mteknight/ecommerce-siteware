using Ecommerce.Domain;
using Ecommerce.Domain.Tests;

using FluentAssertions;

using Xunit;

namespace Ecommerce.Api.Tests;

public class ProductControllerTests
{
    [Theory]
    [MemberData(nameof(ProductTestData.ValidProductTestData), MemberType = typeof(ProductTestData))]
    public void GivenAProduct_WhenAddingNew_ThenReturnProductId(Product product)
    {
        // Arrange
        var sut = new ProductController();

        // Act
        var productId = sut.Add(product);

        // Assert
        productId.Should().NotBe(Guid.Empty, "The new id is expected when adding a new product");
    }
}

public sealed record ProductController
{
    public Guid Add(Product product)
    {
        throw new NotImplementedException();
    }
}
