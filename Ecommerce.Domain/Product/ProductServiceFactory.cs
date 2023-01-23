using Dawn;

using Ecommerce.Common.Domain;

namespace Ecommerce.Domain;

public sealed record ProductServiceFactory : IProductServiceFactory
{
    private readonly IAggregateWriterServiceFactory<Product, ProductValidated> serviceFactory;

    public ProductServiceFactory(IAggregateWriterServiceFactory<Product, ProductValidated> serviceFactory)
    {
        this.serviceFactory = Guard.Argument(serviceFactory, nameof(serviceFactory)).NotNull().Value;
    }

    public IAggregateWriterService<Product, ProductValidated> Create(Product product)
    {
        return new ProductService(product, this.serviceFactory);
    }
}