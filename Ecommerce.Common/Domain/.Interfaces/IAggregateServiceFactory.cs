namespace Ecommerce.Common.Domain;

public interface IAggregateServiceFactory<TAggregateRoot, out TAggregateValidated>
    where TAggregateRoot : class, IAggregateRoot<TAggregateRoot>
    where TAggregateValidated : AggregateValidated<TAggregateRoot>
{
    public IAggregateWriterService<TAggregateRoot, TAggregateValidated> Create(TAggregateRoot aggregateRoot);
}