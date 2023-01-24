namespace Ecommerce.Common.Domain;

public interface IAggregateWriterService<TAggregateRoot, out TAggregateValidated> 
    : IAggregateReadService<TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot<TAggregateRoot>
    where TAggregateValidated : AggregateValidated<TAggregateRoot>
{
    TAggregateValidated ValidatedAggregate { get; }

    Task<bool> Save(CancellationToken cancellationToken = default);
}
