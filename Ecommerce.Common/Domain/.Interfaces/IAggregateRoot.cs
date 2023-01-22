namespace Ecommerce.Common.Domain;

public interface IAggregateRoot<in TAggregateRoot> : IAggregate<TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot<TAggregateRoot>
{
    Guid Id { get; set; }
}