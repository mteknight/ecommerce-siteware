using AutoFixture.Xunit2;

using Ecommerce.Common.Domain;

using FluentAssertions;

using Moq;

using Xunit;

namespace Ecommerce.Domain.Tests;

public sealed record ProductServiceTests
{
    [Theory]
    [AutoData]
    public void GivenValidProduct_WhenAddingNew_ThenReturnNewProductId(Product product)
    {
        // Arrange
        var mockedWriterService = IMockSetup.SetupMockedWriterService();
        var mockedWriterServiceFactory = IMockSetup.SetupMockedWriterServiceFactory(mockedWriterService);
        var sut = GetProductService(product, mockedWriterServiceFactory.Object);
        
        // Act
        var productId = sut.Save();
    
        // Assert
        productId.Should().NotBe(Guid.Empty, "The new id is expected when adding a new product");
        mockedWriterServiceFactory.Verify(factory => factory.Create(It.IsAny<ProductValidated>()), Times.Once);
        mockedWriterService.Verify(service => service.Save(), Times.Once);
    }

    [Theory]
    [MemberData(nameof(ProductTestData.InvalidProductTestData), MemberType = typeof(ProductTestData))]
    public void GivenInvalidProductName_WhenAddingNew_ThenSetInvalidAndReturnEmptyId(Product product)
    {
        // Arrange
        var mockedWriterService = IMockSetup.SetupMockedWriterService();
        var mockedWriterServiceFactory = IMockSetup.SetupMockedWriterServiceFactory(mockedWriterService);
        var sut = GetProductService(product, mockedWriterServiceFactory.Object);
        
        // Act
        var productId = sut.Save();
    
        // Assert
        sut.ValidatedAggregate.IsValid.Should().BeFalse("Expected to be false when validation fails");
        productId.Should().Be(Guid.Empty, "No id should be returned when validation fails");
        mockedWriterServiceFactory.Verify(factory => factory.Create(It.IsAny<ProductValidated>()), Times.Once);
        mockedWriterService.Verify(service => service.Save(), Times.Once);
    }
    
    [Theory]
    [AutoData]
    public void GivenProductDiscount_WhenAddingNew_ThenReturnNewProductWithNoPromotion(Product product)
    {
        // Arrange
        var mockedWriterService = IMockSetup.SetupMockedWriterService();
        var mockedWriterServiceFactory = IMockSetup.SetupMockedWriterServiceFactory(mockedWriterService);
        var sut = GetProductService(product, mockedWriterServiceFactory.Object);
        
        // Act
        var productId = sut.Save();
    
        // Assert
        product.Promotion.Should().NotBeNull("At least the default promotion (NoPromotion) is expected");
        productId.Should().NotBeEmpty("The new id is expected when adding a new product");
        mockedWriterServiceFactory.Verify(factory => factory.Create(It.IsAny<ProductValidated>()), Times.Once);
        mockedWriterService.Verify(service => service.Save(), Times.Once);
    }
    
    [Theory]
    [MemberData(nameof(ProductTestData.ProductWithoutPromotionTestData), MemberType = typeof(ProductTestData))]
    public void GivenProductWithoutPromotion_WhenAddingNew_ThenReturnNewProductWithNoPromotion(Product product)
    {
        // Arrange
        var mockedWriterService = IMockSetup.SetupMockedWriterService();
        var mockedWriterServiceFactory = IMockSetup.SetupMockedWriterServiceFactory(mockedWriterService);
        var sut = GetProductService(product, mockedWriterServiceFactory.Object);
    
        // Act
        var productId = sut.Save();
    
        // Assert
        product.Promotion.Should().BeOfType(typeof(NoPromotion), "The default promotion (NoPromotion) is expected");
        productId.Should().NotBeEmpty("The new id is expected when adding a new product");
        mockedWriterServiceFactory.Verify(factory => factory.Create(It.IsAny<ProductValidated>()), Times.Once);
        mockedWriterService.Verify(service => service.Save(), Times.Once);
    }

    private static IProductService GetProductService(
        Product product,
        IAggregateWriterServiceFactory<Product, ProductValidated> mockedWriterServiceFactory)
    {
        var productServiceFactory = new ProductServiceFactory(mockedWriterServiceFactory);
        
        return productServiceFactory.Create(product);
    }
}
