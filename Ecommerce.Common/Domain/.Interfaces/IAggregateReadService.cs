namespace Ecommerce.Common.Domain;

public interface IAggregateReadService<TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot<TAggregateRoot>
{}