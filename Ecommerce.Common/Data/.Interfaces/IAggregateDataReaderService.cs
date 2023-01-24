using Ecommerce.Common.Domain;

namespace Ecommerce.Common.Data;

public interface IAggregateDataReaderService<TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot<TAggregateRoot>
{
    ValueTask<TAggregateRoot?> Get(
        Guid productId,
        CancellationToken cancellationToken = default);
}
