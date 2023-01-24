using Dawn;

using Ecommerce.Common.Domain;

namespace Ecommerce.Domain;

public interface IProductService : IAggregateRootService
{
    ProductValidated ValidatedAggregate { get; }

    Guid Save();
}

public sealed record ProductService : IProductService
{
    private readonly Product product;
    private readonly IAggregateWriterService<Product,ProductValidated> writerService;
    private ProductValidated? productValidated;

    internal ProductService(
        Product product,
        IAggregateWriterServiceFactory<Product, ProductValidated> serviceFactory)
    {
        this.product = Guard.Argument(product, nameof(product)).NotNull().Value;
        Guard.Argument(serviceFactory, nameof(serviceFactory)).NotNull();
        
        this.writerService = serviceFactory.Create(this.ValidatedAggregate);
    }

    // ReSharper disable once ArrangeObjectCreationWhenTypeNotEvident => Evident enough.
    public ProductValidated ValidatedAggregate => this.productValidated ??= new(this.product);

    public Guid Save()
    {
        var result = this.writerService.Save();

        return result ? this.product.Id : Guid.Empty;
    }
}
