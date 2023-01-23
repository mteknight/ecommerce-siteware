namespace Ecommerce.Common.Domain;

public interface IAggregateWriterServiceFactory<TAggregateRoot, TAggregateValidated>
    where TAggregateRoot : class, IAggregateRoot<TAggregateRoot>
    where TAggregateValidated : AggregateValidated<TAggregateRoot>
{
    IAggregateWriterService<TAggregateRoot, TAggregateValidated> Create(TAggregateValidated validatedAggregate);
}
