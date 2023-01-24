using AutoFixture;

using Dawn;

using Ecommerce.Common.Data;
using Ecommerce.Domain;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;

using Moq;

using Xunit;

namespace Ecommerce.Data.Tests;

public sealed record ProductDataWriterServiceTests
{
    private readonly DbContextOptions options;

    public ProductDataWriterServiceTests(DbContextOptions options)
    {
        this.options = Guard.Argument(options, nameof(options)).NotNull().Value;
    }

    [Theory]
    [MemberData(nameof(ProductDataWriterServiceTestData.ValidProductTestData),
        MemberType = typeof(ProductDataWriterServiceTestData))]
    public void GivenValidProduct_WhenAddingNew_ThenReturnAddedProductWithVerifiedCalls(
        Product product,
        ProductValidated productValidated,
        IFixture fixture)
    {
        // Arrange
        var mockedContext = SetupMockedDbContext(product, fixture, this.options);
        var readerService = new AggregateDataReaderService<Product>(mockedContext.Object);
        var sut = new AggregateDataWriterService<Product, ProductValidated>(mockedContext.Object, readerService);

        // Act
        var wasAdded = sut.Add(productValidated);
        var newProduct = sut.Get(product.Id);

        // Assert
        wasAdded.Should().BeTrue("The new product is expected to be added successfully.");
        newProduct.Should().NotBeNull("The new product was added successfully and should be retrievable.");
        mockedContext.Verify(context => context.Add(It.IsAny<Product>()), Times.Once);
        mockedContext.Verify(context => context.SaveChanges(), Times.Once);
        mockedContext.Verify(context => context.Find<Product>(It.IsAny<Guid>()), Times.Once);
    }

    public static Mock<InMemoryDbContext> SetupMockedDbContext(
        Product product,
        IFixture fixture,
        DbContextOptions? options = default)
    {
        var mockedContext = options is null 
            ? new Mock<InMemoryDbContext>() 
            : new Mock<InMemoryDbContext>(options);
        mockedContext
            .Setup(context => context.Add(It.IsAny<Product>()))
            .Verifiable();

        mockedContext
            .Setup(context => context.SaveChanges())
            .Returns((int)fixture.Create<uint>())
            .Verifiable();

        mockedContext
            .Setup(context => context.Find<Product>(It.IsAny<Guid>()))
            .Returns(product)
            .Verifiable();

        return mockedContext;
    }
}
