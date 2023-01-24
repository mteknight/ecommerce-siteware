namespace Ecommerce.Common.Domain;

public interface IAggregateRootServiceFactory<out TService, in TAggregateRoot>
    where TService : class, IAggregateRootService
    where TAggregateRoot : class, IAggregateRoot<TAggregateRoot>
{
    public TService Create(TAggregateRoot aggregateRoot);
}
