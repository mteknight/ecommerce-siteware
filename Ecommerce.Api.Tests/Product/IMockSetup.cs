using Ecommerce.Domain;

using Moq;

namespace Ecommerce.Api.Tests;

public interface IMockSetup
{
    internal static Mock<IProductService> SetupMockedProductService(Guid expectedId)
    {
        var mockedProductService = new Mock<IProductService>();
        mockedProductService
            .Setup(service => service.Save())
            .Returns(expectedId)
            .Verifiable();
        
        return mockedProductService;
    }

    internal static Mock<IProductServiceFactory> SetupMockedProductServiceFactory(
        IMock<IProductService> mockedProductService)
    {
        var mockedProductServiceFactory = new Mock<IProductServiceFactory>();
        mockedProductServiceFactory
            .Setup(factory => factory.Create(It.IsAny<Domain.Product>()))
            .Returns(mockedProductService.Object)
            .Verifiable();
        
        return mockedProductServiceFactory;
    }
}
