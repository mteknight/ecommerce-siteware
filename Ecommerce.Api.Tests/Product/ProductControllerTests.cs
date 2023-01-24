using AutoFixture;
using AutoFixture.Xunit2;

using Ecommerce.Api.Controllers;
using Ecommerce.Domain;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace Ecommerce.Api.Tests;

public sealed record ProductControllerTests
{
    [Theory]
    [AutoData]
    public void GivenAProduct_WhenAddingNew_ThenReturnProductId(Product product)
    {
        // Arrange
        var fixture = new Fixture();
        var expectedId = fixture.Create<Guid>();
        var mockedProductService = IMockSetup.SetupMockedProductService(expectedId);
        var mockedProductServiceFactory = IMockSetup.SetupMockedProductServiceFactory(mockedProductService);
        var sut = new ProductController(mockedProductServiceFactory.Object);

        // Act
        var request = sut.Add(product);

        // Assert
        request.Should().NotBeNull("A result is expected.");
        request.Result.Should().BeOfType<OkObjectResult>("Ok is expected when request is successful.");
        request.Result.As<OkObjectResult>().Value.Should().Be(expectedId, "The new id is expected when adding a new product");
        mockedProductServiceFactory.Verify(factory => factory.Create(It.IsAny<Product>()), Times.Once);
        mockedProductService.Verify(service => service.Save(), Times.Once);
    }
    
    [Theory]
    [InlineData(default)]
    public void GivenNullProduct_WhenAddingNew_ThenReturnBadRequest(Product product)
    {
        // Arrange
        var fixture = new Fixture();
        var expectedId = fixture.Create<Guid>();
        var mockedProductService = IMockSetup.SetupMockedProductService(expectedId);
        var mockedProductServiceFactory = IMockSetup.SetupMockedProductServiceFactory(mockedProductService);
        var sut = new ProductController(mockedProductServiceFactory.Object);
    
        // Act
        var request = sut.Add(product);
        
        // Assert
        request.Should().NotBeNull("A result is expected.");
        request.Result.Should().BeOfType<BadRequestResult>("BedRequest is expected when mandatory parameter is null.");
    }

    [Theory]
    [MemberData(nameof(Domain.Tests.ProductTestData.InvalidProductTestData), MemberType = typeof(Domain.Tests.ProductTestData))]
    public void GivenInvalidProduct_WhenAddingNew_ThenReturnEmptyId(Product product)
    {
        // Arrange
        var expectedId = Guid.Empty;
        var mockedProductService = IMockSetup.SetupMockedProductService(expectedId);
        var mockedProductServiceFactory = IMockSetup.SetupMockedProductServiceFactory(mockedProductService);
        var sut = new ProductController(mockedProductServiceFactory.Object);
    
        // Act
        var request = sut.Add(product);
        
        // Assert
        request.Should().NotBeNull("A result is expected.");
        request.Result.Should().BeOfType<OkObjectResult>("Ok is expected when request is successful.");
        request.Result.As<OkObjectResult>().Value.Should().Be(expectedId, "The new id is expected when adding a new product");
        mockedProductServiceFactory.Verify(factory => factory.Create(It.IsAny<Product>()), Times.Once);
        mockedProductService.Verify(service => service.Save(), Times.Once);
        
    }
}
