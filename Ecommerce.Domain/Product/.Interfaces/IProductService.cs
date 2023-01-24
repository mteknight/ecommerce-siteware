using Ecommerce.Common.Domain;

namespace Ecommerce.Domain;

public interface IProductService : IAggregateRootService
{
    ProductValidated ValidatedAggregate { get; }

    Task<Guid> Save(CancellationToken cancellationToken = default);
}