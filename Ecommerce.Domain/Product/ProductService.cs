using Dawn;

using Ecommerce.Common.Domain;

namespace Ecommerce.Domain;

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

    public async Task<Guid> Save(CancellationToken cancellationToken = default)
    {
        var result = await this.writerService.Save(cancellationToken);

        return result ? this.product.Id : Guid.Empty;
    }
}
