using Ecommerce.Common.Domain;

using Moq;

namespace Ecommerce.Domain.Tests;

public interface IMockSetup
{
    internal static Mock<IAggregateWriterService<Product, ProductValidated>> SetupMockedWriterService()
    {
        var mockedWriterService = new Mock<IAggregateWriterService<Product, ProductValidated>>();
        mockedWriterService
            .Setup(service => service.Save())
            .Returns(true)
            .Verifiable();

        return mockedWriterService;
    }

    internal static Mock<IAggregateWriterServiceFactory<Product, ProductValidated>> SetupMockedWriterServiceFactory(
        IMock<IAggregateWriterService<Product, ProductValidated>> mockedWriterService)
    {
        var mockedWriterServiceFactory = new Mock<IAggregateWriterServiceFactory<Product, ProductValidated>>();
        mockedWriterServiceFactory
            .Setup(factory => factory.Create(It.IsAny<ProductValidated>()))
            .Returns(mockedWriterService.Object)
            .Verifiable();

        return mockedWriterServiceFactory;
    }
}
