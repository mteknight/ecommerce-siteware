using JetBrains.Annotations;

namespace Ecommerce.Common.Domain;

[UsedImplicitly]
public sealed record AggregateWriterServiceFactory<TAggregateRoot, TAggregateValidated>
    : IAggregateWriterServiceFactory<TAggregateRoot, TAggregateValidated>
    where TAggregateRoot : class, IAggregateRoot<TAggregateRoot>
    where TAggregateValidated : AggregateValidated<TAggregateRoot>
{
    public IAggregateWriterService<TAggregateRoot, TAggregateValidated> Create(TAggregateValidated validatedAggregate)
    {
        return new AggregateWriterService<TAggregateRoot, TAggregateValidated>(validatedAggregate);
    }
}
