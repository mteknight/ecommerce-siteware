using Ecommerce.Common.Domain;

namespace Ecommerce.Common.Data;

public interface IAggregateDataWriterService<TAggregateRoot, in TAggregateValidated>
    : IAggregateDataReaderService<TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot<TAggregateRoot>
    where TAggregateValidated : AggregateValidated<TAggregateRoot>
{
    Task<bool> Add(
        TAggregateValidated productValidated,
        CancellationToken cancellationToken = default);
}
