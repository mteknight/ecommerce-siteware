using AutoFixture;

using Ecommerce.Common.Data;
using Ecommerce.Domain;

using FluentAssertions;

using Moq;

using Xunit;

namespace Ecommerce.Data.Tests;

public sealed record ProductDataWriterServiceTests
{
    [Theory]
    [MemberData(nameof(ProductDataWriterServiceTestData.ValidProductTestData),
        MemberType = typeof(ProductDataWriterServiceTestData))]
    public async Task GivenValidProduct_WhenAddingNew_ThenReturnAddedProductWithVerifiedCalls(
        Product product,
        ProductValidated productValidated,
        IFixture fixture)
    {
        // Arrange
        var mockedContext = SetupMockedDbContext(product, fixture);
        var readerService = new AggregateDataReaderService<Product>(mockedContext.Object);
        var sut = new AggregateDataWriterService<Product, ProductValidated>(mockedContext.Object, readerService);

        // Act
        var wasAdded = await sut.Add(productValidated);
        var newProduct = await sut.Get(product.Id);

        // Assert
        wasAdded.Should().BeTrue("The new product is expected to be added successfully.");
        newProduct.Should().NotBeNull("The new product was added successfully and should be retrievable.");
        mockedContext.Verify(context => context.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Once);
        mockedContext.Verify(context => context.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        mockedContext.Verify(context => context.FindAsync<Product>(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    public static Mock<InMemoryDbContext> SetupMockedDbContext(
        Product product,
        IFixture fixture)
    {
        var mockedContext = new Mock<InMemoryDbContext>();
        mockedContext
            .Setup(context => context.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
            .Verifiable();

        mockedContext
            .Setup(context => context.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync((int)fixture.Create<uint>())
            .Verifiable();

        mockedContext
            .Setup(context => context.FindAsync<Product>(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(product)
            .Verifiable();

        return mockedContext;
    }
}
