namespace Ecommerce.Common.Domain;

public interface IAggregate<in TAggregate>
    where TAggregate : class, IAggregate<TAggregate>
{}
