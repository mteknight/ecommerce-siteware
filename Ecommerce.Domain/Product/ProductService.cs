using Dawn;

using Ecommerce.Common.Domain;

namespace Ecommerce.Domain;

public sealed record ProductService : IAggregateWriterService<Product, ProductValidated>
{
    private readonly Product product;
    private readonly IAggregateWriterServiceFactory<Product, ProductValidated> serviceFactory;
    private ProductValidated? productValidated;

    internal ProductService(
        Product product,
        IAggregateWriterServiceFactory<Product, ProductValidated> serviceFactory)
    {
        this.product = Guard.Argument(product, nameof(product)).NotNull().Value;
        this.serviceFactory = Guard.Argument(serviceFactory, nameof(serviceFactory)).NotNull().Value;
    }

    // ReSharper disable once ArrangeObjectCreationWhenTypeNotEvident => Evident enough.
    public ProductValidated ValidatedAggregate => this.productValidated ??= new(this.product);

    public Guid Save()
    {
        var service = this.serviceFactory.Create(this.ValidatedAggregate);
        return service.Save();
    }
}
